using BisCAD.ParametricModels;
using BisCAD.ParametricModels.Curves;
using BisCAD.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BisCAD.UserControls
{
    public partial class MultiBezierCurveControl : UserControl
    {
        public MultiBezierCurveModel _bezierCurve;
        public CursorModel _cursor;

        List<Vector3> _initialPointPositions;
        public bool AddPointOnClick = false;

        public MultiBezierCurveControl(MultiBezierCurveModel bezier, CursorModel cursor)
        {
            InitializeComponent();

            _cursor = cursor;
            _bezierCurve = bezier;
            _initialPointPositions = new List<Vector3>();
            _bezierCurve.BernsteinPoints.ForEach(point =>
            {
                _initialPointPositions.Add(point.Position);
            });

            pointsListBox.DataSource = _bezierCurve.BernsteinPoints;
            pointsListBox.DisplayMember = "DisplayName";

            CreateBindings();
        }


        private void CreateBindings()
        {
            textboxName.DataBindings.Add("Text", _bezierCurve, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            labelPartitionsCount.DataBindings.Add("Text", _bezierCurve, "Partitions", true, DataSourceUpdateMode.OnPropertyChanged);
            showPolygonCheckbox.DataBindings.Add("Checked", _bezierCurve, "ShowBernsteinPolygon", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void removePointButton_Click(object sender, EventArgs e)
        {
            if (pointsListBox.SelectedIndex < 0)
                return;

            var point = _bezierCurve.BernsteinPoints[pointsListBox.SelectedIndex];
            RemovePointFromCurve(point);
            _bezierCurve.Update();
            RefreshPointsListBox();
        }


        public void RefreshPointsListBox()
        {
            pointsListBox.DataSource = null;
            pointsListBox.DataSource = _bezierCurve.BernsteinPoints;
            pointsListBox.DisplayMember = "DisplayName";
            pointsListBox.Refresh();
        }


        private void pointsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pointsListBox.SelectedIndex < 0) return;

            var selectedPoint = _bezierCurve.BernsteinPoints[pointsListBox.SelectedIndex];
            _cursor.PositionX = selectedPoint.PositionX;
            _cursor.PositionY = selectedPoint.PositionY;
            _cursor.PositionZ = selectedPoint.PositionZ;
        }


        public void AddPointToCurve(IParametricModel point)
        {
            _bezierCurve.BernsteinPoints.Add(point);
            _initialPointPositions.Add(point.Position);
            point.Update();
        }

        public void RemovePointFromCurve(IParametricModel point)
        {
            _bezierCurve.BernsteinPoints.Remove(point);
            _initialPointPositions.Remove(point.Position);
        }

        private void RefreshOnValueChanged(object sender, System.EventArgs e)
        {
            (sender as Control).Refresh();

            //Global.mainForm.RecalculateCursorPosition();
            //CalculateCursorScreenPosition();

            CalculateCursorCenterForBezier();

            for(int i = 0; i < _bezierCurve.BernsteinPoints.Count; i++)
            {
                var initialPosition = _initialPointPositions[i];
                var point = _bezierCurve.BernsteinPoints[i];
               

                float translationX = (float)numericPositionX.Value;
                float translationY = (float)numericPositionY.Value;
                float translationZ = (float)numericPositionZ.Value;

                float rotationX = (float)numericRotationX.Value;
                float rotationY = (float)numericRotationY.Value;
                float rotationZ = (float)numericRotationZ.Value;

                float scaleX = (float)numericScaleX.Value;
                float scaleY = (float)numericScaleY.Value;
                float scaleZ = (float)numericScaleZ.Value;

                Vector3 cursorPos = _cursor.Position;
                Vector3 modelPos = initialPosition;
            
                Matrix4 transform = Matrix4.CreateTranslation(modelPos - cursorPos)
                * Matrix4.CreateScale(scaleX, scaleY, scaleZ)
                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationZ))
                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY))
                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationX))
                * Matrix4.CreateTranslation(cursorPos - modelPos);

                var newPoint = new Vector4(initialPosition, 1) * transform;
                newPoint /= newPoint.W;

                point.PositionX = newPoint.X + translationX;
                point.PositionY = newPoint.Y + translationY;
                point.PositionZ = newPoint.Z + translationZ;            
            }
            _bezierCurve.Update();
            labelPartitionsCount.Text = _bezierCurve.Partitions;
            labelPartitionsCount.Refresh();
        }

        
        
        //TODO:Refactor this to dataBinding
        private void addPointOnClickCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            AddPointOnClick = addPointOnClickCheckbox.Checked;
        }

        private void CalculateCursorCenterForBezier()
        {
            if (_initialPointPositions.Count == 0) return;
            var position = Vector3.Zero;
            _initialPointPositions.ForEach(x =>
            {
                position += x;
            });
            position /= _initialPointPositions.Count;

            _cursor.PositionX = position.X;
            _cursor.PositionY = position.Y;
            _cursor.PositionZ = position.Z;
        }
    }
}
