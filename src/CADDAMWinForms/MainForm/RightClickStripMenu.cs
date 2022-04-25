using BisCAD.ParametricModels;
using BisCAD.UserControls;
using BisCAD.Utils;
using OpenTK;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BisCAD
{
    public partial class MainForm
    {
        private ContextMenuStrip listboxContextMenu;
        private IParametricModel objectToAdd = null;
        private void listBoxElements_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var selectedIdx = listBoxElements.IndexFromPoint(e.Location);

                //found element
                if (selectedIdx != -1)
                {
                    var uc = GetCurrentUserControl();
                    if (uc is MultiBezierCurveControl || uc is MultiBezierCurveC2Control)
                    {
                        var obj = _scene.ParametricModels[selectedIdx];

                        //allow to add only points
                        if (!(obj is PointModel))
                            return;

                        objectToAdd = obj;
                        showContextMenu();
                    }
                }
            }
        }

        private void showContextMenu()
        {
            //clear the menu and add custom items
            listboxContextMenu.Items.Clear();
            listboxContextMenu.Items.Add("Add to Bezier Curve");
            listboxContextMenu.Items[0].MouseDown += new MouseEventHandler(listboxContextMenu_Clicked);
        }

        private void listboxContextMenu_Clicked(object sender, MouseEventArgs e)
        {
            var uc = GetCurrentUserControl();

            if (uc is MultiBezierCurveControl)
            {
                var bezierControl = uc as MultiBezierCurveControl;
                bezierControl.AddPointToCurve(objectToAdd);
                bezierControl._bezierCurve.Update();
                bezierControl.RefreshPointsListBox();
            }
            else if(uc is MultiBezierCurveC2Control)
            {
                var bezierControl = uc as MultiBezierCurveC2Control;
                bezierControl.AddPointToCurve(objectToAdd);
                bezierControl._bezierCurve.Update();
                bezierControl.RefreshPointsListBox();
            }


        }

        public UserControl GetCurrentUserControl()
        {
            if (userControlPanel.Controls.Count == 0) 
                return null;

            if (userControlPanel.Controls.Count > 1) 
                throw new Exception();

            return userControlPanel.Controls[0] as UserControl;
        }
    }
}
