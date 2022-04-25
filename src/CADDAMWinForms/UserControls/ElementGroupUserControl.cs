using BisCAD.ParametricModels;
using BisCAD.Utils;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BisCAD.UserControls
{
    public partial class ElementGroupUserControl : UserControl
    {
        public List<IParametricModel> _selectedItems { get; set; }
        public IParametricModel _cursor { get; set; }

        List<Vector3> _initialPointsPositions;
        Vector3 _initialCursorPosition;

        public ElementGroupUserControl(List<IParametricModel> models, IParametricModel cursorModel)
        {
            InitializeComponent();
            _selectedItems = models;
            _cursor = cursorModel;
            _initialPointsPositions = new List<Vector3>();
            _initialCursorPosition = _cursor.Position;
            CreateBindings();

            _selectedItems.ForEach(model => { if (model is PointModel) _initialPointsPositions.Add(model.Position); });
            doNotRefreshUpDowns = false;
        }


        private void CreateBindings()
        { 
            if (_selectedItems.Count == 0)
            {
                textboxName.Text = "Cursor";
                groupBoxRotation.Visible = false;
                groupBoxScale.Visible = false;
            }          
            else
                textboxName.Text = $"{_selectedItems.Count} items group";

            //numericUpDowns
            //numericPositionX.DataBindings.Add("Value", _cursor, "PositionX", true, DataSourceUpdateMode.OnPropertyChanged);
            //numericPositionY.DataBindings.Add("Value", _cursor, "PositionY", true, DataSourceUpdateMode.OnPropertyChanged);
            //numericPositionZ.DataBindings.Add("Value", _cursor, "PositionZ", true, DataSourceUpdateMode.OnPropertyChanged);
            numericPositionX.Value = (decimal)_cursor.PositionX;
            numericPositionY.Value = (decimal)_cursor.PositionY;
            numericPositionZ.Value = (decimal)_cursor.PositionZ;

            CalculateCursorScreenPosition();
        }

        bool doNotRefreshUpDowns = true;

        private void RefreshOnValueChanged(object sender, System.EventArgs e)
        {
            if (doNotRefreshUpDowns) return;
            (sender as Control).Refresh();

            Global.mainForm.RecalculateCursorPosition();
            CalculateCursorScreenPosition();

            //it is needed to access good memory
            int pointIdx = 0;
            _selectedItems.ForEach(model =>
            {
                CalculateCursorScreenPosition();
                Matrix4 transform = Matrix4.Identity;

                float translationX = (float)numericPositionX.Value;
                float translationY = (float)numericPositionY.Value;
                float translationZ = (float)numericPositionZ.Value;

                float rotationX = (float)numericRotationX.Value;
                float rotationY = (float)numericRotationY.Value;
                float rotationZ = (float)numericRotationZ.Value;

                float scaleX = (float)numericScaleX.Value;
                float scaleY = (float)numericScaleY.Value;
                float scaleZ = (float)numericScaleZ.Value;

                Vector3 modelPos = model.Position;
                Vector3 cursorPos = _initialCursorPosition;

                transform = Matrix4.CreateTranslation(modelPos - cursorPos)
                * Matrix4.CreateScale(scaleX, scaleY, scaleZ)
                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationZ))
                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY))
                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationX))
                * Matrix4.CreateTranslation(cursorPos - modelPos);


                if (model is PointModel)
                {
                    var initialPos = _initialPointsPositions[pointIdx++];


                    //modelPos = initialPos;
                    transform = Matrix4.CreateTranslation(initialPos - cursorPos)
                                * Matrix4.CreateScale(scaleX, scaleY, scaleZ)
                                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotationZ))
                                * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotationY))
                                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotationX))
                                * Matrix4.CreateTranslation(cursorPos - initialPos);


                    var newPoint = new Vector4(initialPos, 1) * transform;
                    newPoint /= newPoint.W;

                    model.PositionX = newPoint.X;
                    model.PositionY = newPoint.Y;
                    model.PositionZ = newPoint.Z;
                }
                else
                {
                    model.PrevModel = transform;
                }
            });
            Global.mainForm.UpdateAllBezierCurves();
        }

        public void UpdatePositionUpDowns(Vector3 position)
        {
            numericPositionX.Value += (decimal)position.X;
            numericPositionY.Value += (decimal)position.Y;
            numericPositionZ.Value += (decimal)position.Z;
        }

        public void CalculateCursorScreenPosition()
        {       
            Vector4 pointPosition = new Vector4(_cursor.PositionX, _cursor.PositionY, _cursor.PositionZ, 1);
            var position = pointPosition * Global.Camera.GetViewMatrix() * Global.Camera.GetProjectionMatrix();
            position /= position.W;

            numericScreenPositionX.Value = (decimal)MathHelper.Clamp(position.X, -10, 10);
            numericScreenPositionY.Value = (decimal)MathHelper.Clamp(position.Y, -10, 10);
            numericScreenPositionX.Refresh();
            numericScreenPositionY.Refresh();
        }

    }
}
