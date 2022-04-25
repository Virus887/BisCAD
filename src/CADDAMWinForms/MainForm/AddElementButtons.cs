using BisCAD.ParametricModels;
using BisCAD.ParametricModels.Curves;
using BisCAD.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BisCAD
{
    public partial class MainForm
    {
        private void addTorusButton_Click(object sender, EventArgs e)
        {
            var torus = new TorusParametricModel(1, 0.5f, 12, 20)
            {
                PositionX = _scene.Cursor.PositionX,
                PositionY = _scene.Cursor.PositionY,
                PositionZ = _scene.Cursor.PositionZ
            };
            _scene.ParametricModels.Add(torus);
            TorusUserControl uc = new TorusUserControl(torus);
            AddUserControl(uc);
            listBoxElements.ClearSelected();
            listBoxElements.SelectedIndex = _scene.ParametricModels.Count - 1;
        }

        private void addPointButton_Click(object sender, EventArgs e)
        {
            var point = new PointModel()
            {
                PositionX = _scene.Cursor.PositionX,
                PositionY = _scene.Cursor.PositionY,
                PositionZ = _scene.Cursor.PositionZ
            };
            _scene.ParametricModels.Add(point);
            PointUserControl uc = new PointUserControl(point);
            AddUserControl(uc);
            listBoxElements.ClearSelected();
            listBoxElements.SelectedIndex = _scene.ParametricModels.Count - 1;
        }


        private void addBezierCurve_Click(object sender, EventArgs e)
        {
            var selectedItems = GetSelectedObjectsList().Where(x => x is PointModel).ToList();
            var curve = new MultiBezierCurveModel(selectedItems);
            _scene.ParametricModels.Add(curve);
            MultiBezierCurveControl uc = new MultiBezierCurveControl(curve, _scene.Cursor as CursorModel);
            AddUserControl(uc);
            listBoxElements.ClearSelected();
            listBoxElements.SelectedIndex = _scene.ParametricModels.Count - 1;
        }


        private void addBezierCurveC2_Click(object sender, EventArgs e)
        {
            var selectedItems = GetSelectedObjectsList().Where(x => x is PointModel).ToList();
            var curve = new MultiBezierCurveC2Model(selectedItems);
            _scene.ParametricModels.Add(curve);
            MultiBezierCurveC2Control uc = new MultiBezierCurveC2Control(curve, _scene.Cursor as CursorModel);
            AddUserControl(uc);
            listBoxElements.ClearSelected();
            listBoxElements.SelectedIndex = _scene.ParametricModels.Count - 1;
        }



        private void AddUserControl(UserControl userControl)
        {
            //userControlPanel
            userControl.Dock = DockStyle.Fill;

            if (userControlPanel.Controls.Count > 0) userControlPanel.Controls[0].Dispose();
            userControlPanel.Controls.Clear();
            userControlPanel.Controls.Add(userControl);
            userControl.BringToFront();
            //listBox
            RefreshElementsListBox();
            Refresh();
        }


    }
}
