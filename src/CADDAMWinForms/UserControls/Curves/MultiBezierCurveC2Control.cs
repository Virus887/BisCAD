using BisCAD.ParametricModels;
using BisCAD.ParametricModels.Curves;
using BisCAD.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BisCAD.UserControls
{
    public partial class MultiBezierCurveC2Control : UserControl
    {
        public MultiBezierCurveC2Model _bezierCurve;
        public CursorModel _cursor;

        List<Vector3> _initialDeBoorePositions;

        public bool AddPointOnClick = false;

        public IParametricModel SelectedBernsteinPoint { get; set; } = null;

        public MultiBezierCurveC2Control(MultiBezierCurveC2Model bezier, CursorModel cursor)
        {
            InitializeComponent();

            _cursor = cursor;
            _bezierCurve = bezier;

            //create initial points positions
            _initialDeBoorePositions = new List<Vector3>();
            _bezierCurve.DeBoorePoints.ForEach(point => _initialDeBoorePositions.Add(point.Position));

            lBoxDeBoorePoints.DataSource = _bezierCurve.DeBoorePoints;
            lBoxDeBoorePoints.DisplayMember = "DisplayName";
            lBoxBensteinPoints.DataSource = _bezierCurve.BernsteinPoints;
            lBoxBensteinPoints.DisplayMember = "DisplayName";

            CreateBindings();
        }


        private void CreateBindings()
        {
            textboxName.DataBindings.Add("Text", _bezierCurve, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            labelPartitionsCount.DataBindings.Add("Text", _bezierCurve, "Partitions", true, DataSourceUpdateMode.OnPropertyChanged);
            chbShowBernsteinPolygon.DataBindings.Add("Checked", _bezierCurve, "ShowBernsteinPolygon", true, DataSourceUpdateMode.OnPropertyChanged);
            chbShowDeBoorePolygon.DataBindings.Add("Checked", _bezierCurve, "ShowDeBoorePolygon", true, DataSourceUpdateMode.OnPropertyChanged);
            chbShowBernsteinPoints.DataBindings.Add("Checked", _bezierCurve, "ShowBernsteinPoints", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void removePointButton_Click(object sender, EventArgs e)
        {
            if (lBoxDeBoorePoints.SelectedIndex < 0)
                return;

            var point = _bezierCurve.DeBoorePoints[lBoxDeBoorePoints.SelectedIndex];
            RemovePointFromCurve(point);
            _bezierCurve.Update();
            RefreshPointsListBox();
        }


        public void RefreshPointsListBox()
        {
            lBoxDeBoorePoints.DataSource = null;
            lBoxDeBoorePoints.DataSource = _bezierCurve.DeBoorePoints;
            lBoxDeBoorePoints.DisplayMember = "DisplayName";
            lBoxDeBoorePoints.Refresh();

            lBoxBensteinPoints.DataSource = null;
            lBoxBensteinPoints.DataSource = _bezierCurve.BernsteinPoints;
            lBoxBensteinPoints.DisplayMember = "DisplayName";
            lBoxBensteinPoints.Refresh();
        }


        public void AddPointToCurve(IParametricModel point)
        {
            _bezierCurve.DeBoorePoints.Add(point);
            _initialDeBoorePositions.Add(point.Position);
            //_bezierCurve.CreateBernsteinPoints();

            //TODO: Point update should me in another place
            point.Update();
            _bezierCurve.Update();
        }

        public void RemovePointFromCurve(IParametricModel point)
        {
            _bezierCurve.DeBoorePoints.Remove(point);
            _initialDeBoorePositions.Remove(point.Position);
            //_bezierCurve.CreateBernsteinPoints();

            _bezierCurve.Update();
        }



        //TODO:Refactor this to dataBinding
        private void addPointOnClickCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            AddPointOnClick = addPointOnClickCheckbox.Checked;
        }

        private void CalculateCursorCenterForBezier()
        {
            if (_initialDeBoorePositions.Count == 0) return;
            var position = Vector3.Zero;
            _initialDeBoorePositions.ForEach(x =>
            {
                position += x;
            });
            position /= _initialDeBoorePositions.Count;

            _cursor.PositionX = position.X;
            _cursor.PositionY = position.Y;
            _cursor.PositionZ = position.Z;
        }


        private void RefreshOnValueChanged(object sender, System.EventArgs e)
        {
            (sender as Control).Refresh();

            //Global.mainForm.RecalculateCursorPosition();
            //CalculateCursorScreenPosition();

            CalculateCursorCenterForBezier();

            for (int i = 0; i < _bezierCurve.DeBoorePoints.Count; i++)
            {
                var initialPosition = _initialDeBoorePositions[i];
                var point = _bezierCurve.DeBoorePoints[i];


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


        private void lBoxDeBoorePoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lBoxDeBoorePoints.SelectedIndex < 0) return;

            var selectedPoint = _bezierCurve.DeBoorePoints[lBoxDeBoorePoints.SelectedIndex];
            _cursor.Position = selectedPoint.Position;
            _cursor.Update();
        }

        private void lBoxBensteinPoints_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lBoxBensteinPoints_Click(object sender, EventArgs e)
        {
            if (lBoxBensteinPoints.SelectedIndex < 0)            
                SelectedBernsteinPoint = null;
            
            if(SelectedBernsteinPoint != null)
                SelectedBernsteinPoint.IsSelected = false;

            //disable checkbox
            AddPointOnClick = false;
            addPointOnClickCheckbox.Checked = false;

            SelectedBernsteinPoint = _bezierCurve.BernsteinPoints[lBoxBensteinPoints.SelectedIndex];
            _bezierCurve.BernsteinPoints.ForEach(x => x.IsSelected = false);
            SelectedBernsteinPoint.IsSelected = true;

            _cursor.Position = SelectedBernsteinPoint.Position;
            _cursor.Update();
        }


        public void HandleBernsteinPointMovement(Vector2 deltaMove)
        {
            if (SelectedBernsteinPoint == null) return;

            int selectedIdx = lBoxBensteinPoints.SelectedIndex;

            switch(selectedIdx % 3)
            {
                case 0:
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position += Global.Camera.Right * deltaMove.X;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position -= Global.Camera.Up * deltaMove.Y;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Update();

                    _initialDeBoorePositions[selectedIdx / 3 + 1] += Global.Camera.Right * deltaMove.X;
                    _initialDeBoorePositions[selectedIdx / 3 + 1] -= Global.Camera.Up * deltaMove.Y;
                    break;


                    //posrednie
                case 1:
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position += Global.Camera.Right * deltaMove.X * 2/3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position -= Global.Camera.Up * deltaMove.Y * 2 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Update();
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Position += Global.Camera.Right * deltaMove.X * 1 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Position -= Global.Camera.Up * deltaMove.Y * 1 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Update();

                    _initialDeBoorePositions[selectedIdx / 3 + 1] += Global.Camera.Right * deltaMove.X * 2 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 1] -= Global.Camera.Up * deltaMove.Y * 2 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 2] += Global.Camera.Right * deltaMove.X * 1 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 2] -= Global.Camera.Up * deltaMove.Y * 1 / 3.0f;
                    break;

                case 2:
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position += Global.Camera.Right * deltaMove.X * 1 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Position -= Global.Camera.Up * deltaMove.Y * 1 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 1].Update();
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Position += Global.Camera.Right * deltaMove.X * 2 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Position -= Global.Camera.Up * deltaMove.Y * 2 / 3.0f;
                    _bezierCurve.DeBoorePoints[selectedIdx / 3 + 2].Update();

                    _initialDeBoorePositions[selectedIdx / 3 + 1] += Global.Camera.Right * deltaMove.X * 1 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 1] -= Global.Camera.Up * deltaMove.Y * 1 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 2] += Global.Camera.Right * deltaMove.X * 2 / 3.0f;
                    _initialDeBoorePositions[selectedIdx / 3 + 2] -= Global.Camera.Up * deltaMove.Y * 2 / 3.0f;
                    break;
            }
           

            Global.mainForm.UpdateAllBezierCurves();
        }
    }
}
