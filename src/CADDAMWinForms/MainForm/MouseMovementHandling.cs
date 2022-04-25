using BisCAD.ParametricModels;
using BisCAD.ParametricModels.Curves;
using BisCAD.UserControls;
using BisCAD.Utils;
using OpenTK;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace BisCAD
{
    //TODO: Divide this code to MouseHandling and KeyboardHandling
    public partial class MainForm
    {
        private float lastTime;
        private bool LeftMouseDown = false;
        private bool RightMouseDown = false;
        private bool MiddleMouseDown = false;
       
        private Point previousPosition = new Point();

        private bool ctrlPressed = false;


        private void glControl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Global.Camera.Position += Global.Camera.Front * e.Delta * 0.0005f;
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    LeftMouseDown = true;
                    break;
                case MouseButtons.Right:
                    RightMouseDown = true;
                    break;
                case MouseButtons.Middle:
                    MiddleMouseDown = true;
                    break;
                default:
                    return;
            }
            if (Global.mainForm.userControlPanel.Controls.Count > 0)
            {
                var uc = Global.mainForm.userControlPanel.Controls[0];
                if (uc is ElementGroupUserControl)
                {
                    (uc as ElementGroupUserControl).CalculateCursorScreenPosition();
                }
            }
            
            previousPosition = e.Location;
        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    LeftMouseDown = false;
                    var uc = GetCurrentUserControl();
                    if(uc is MultiBezierCurveC2Control)
                        (uc as MultiBezierCurveC2Control).RefreshPointsListBox();
                    RefreshElementsListBox();
                    break;
                case MouseButtons.Right:
                    RightMouseDown = false;
                    break;
                case MouseButtons.Middle:
                    MiddleMouseDown = false;
                    break;
                default:
                    return;
            }
        }

        private Vector3 prevPos;
        private float prevYaw;
        private float cumulativeAngle;

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            var c = Global.Camera;

            float time = (float)Global.Timer.Elapsed.TotalSeconds;
            float elapsed = time - lastTime;
            lastTime = time;
            float speed = 0.3f;
            var deltaMove = new PointF(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
            deltaMove.X *= speed * elapsed;
            deltaMove.Y *= speed * elapsed;

            if (MiddleMouseDown)
            {
                Global.Camera.Position -= Global.Camera.Right * deltaMove.X;
                Global.Camera.Position += Global.Camera.Up * deltaMove.Y;
            }


            if (LeftMouseDown)
            {
                var selectedItems = GetSelectedObjectsList(); 
                if(selectedItems.Count == 0)
                {
                    Global.Camera.Yaw -= deltaMove.X * 50;
                    Global.Camera.Pitch += deltaMove.Y * 50;
                }
                else
                {
                    //We are looking for C2 curve:
                    UserControl uc = GetCurrentUserControl();
                    if (uc is MultiBezierCurveC2Control)
                    {
                        var C2Curve = uc as MultiBezierCurveC2Control;
                        if(C2Curve.SelectedBernsteinPoint != null)
                        {
                            C2Curve.HandleBernsteinPointMovement(new Vector2(deltaMove.X, deltaMove.Y)/3);
                        }
                    }
                    else
                    {
                        selectedItems.ForEach(x =>
                        {
                            x.Position += Global.Camera.Right * deltaMove.X / 3;
                            x.Position -= Global.Camera.Up * deltaMove.Y / 3;
                            x.Update();
                        });
                        UpdateAllBezierCurves();
                    }
                }
            }



            if (RightMouseDown)
            {
                //deltaMove.X = MathHelper.RadiansToDegrees(deltaMove.X);
                cumulativeAngle += deltaMove.X;
                float oldY = c.Position.Y;
                c.Position = prevPos + (-c.Front + c.Front * (float)Math.Cos(cumulativeAngle) + c.Right * (float)Math.Sin(cumulativeAngle)) * 1.0f;
                c.Position = new Vector3(c.Position.X, oldY, c.Position.Z);
                c.Yaw = prevYaw - MathHelper.RadiansToDegrees(cumulativeAngle);
            }
            else
            {
                prevPos = c.Position;
                prevYaw = c.Yaw;
                cumulativeAngle = 0;
            }

            var beziercurves = _scene.ParametricModels.Where(x => x is MultiBezierCurveModel).Select(x => x as MultiBezierCurveModel).ToList();
            beziercurves.ForEach(x => x.Update());

            previousPosition = e.Location;
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            var c = Global.Camera;
            float time = (float)Global.Timer.Elapsed.TotalSeconds;
            float elapsed = time - lastTime;
            lastTime = time;

            if (e.KeyCode == Keys.W)
                c.Position += c.Front * elapsed;
            if (e.KeyCode == Keys.S)
                c.Position -= c.Front *  elapsed;
            if (e.KeyCode == Keys.A)
                c.Position -= c.Right * elapsed;
            if (e.KeyCode == Keys.D)
                c.Position += c.Right *  elapsed;

            if (e.KeyCode == Keys.ControlKey)
                ctrlPressed = true;

        }

        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                ctrlPressed = false;
        }


        private void glControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (var model in _scene.ParametricModels)
                {
                    if (model is PointModel)
                    {
                        //[0, WIDTH] -> [-1, 1]
                        var mousePosition = new PointF((e.X * 2.0f / glControl.Width) - 1.0f, -(e.Y * 2.0f / glControl.Height) + 1.0f);

                        Vector4 modelPosition = new Vector4(model.PositionX, model.PositionY, model.PositionZ, 1);

                        var modelPositionOnScreen = modelPosition * Global.Camera.GetViewMatrix() * Global.Camera.GetProjectionMatrix();
                        modelPositionOnScreen /= modelPositionOnScreen.W;

                        //TODO: zaznacz tylko ten jeden element a nie dodaj do zaznaczonych
                        if (ArePointsNear2D(mousePosition, modelPositionOnScreen))
                        {
                            if(ctrlPressed)
                            {
                                int modelIdx = _scene.ParametricModels.IndexOf(model);
                                if(!_scene.ParametricModels[modelIdx].IsSelected)
                                    listBoxElements.SetSelected(modelIdx, true);
                                else
                                    listBoxElements.SetSelected(modelIdx, false);
                            }


                            //byćmoże return potrzebny
                            //return;
                        }
                    }
                }
                //Calculate clicked point
                //TODO: do if C pressed! (or something clever)
                var viewProjInv = Global.Camera.GetViewMatrix() * Global.Camera.GetProjectionMatrix();
                var cursorNowPosition = new Vector4(_scene.Cursor.Position, 1) * viewProjInv;
                cursorNowPosition /= cursorNowPosition.W;

                var mousePosition1 = new PointF((e.X * 2.0f / glControl.Width) - 1.0f, -(e.Y * 2.0f / glControl.Height) + 1.0f);
                var psCords = new Vector4(mousePosition1.X, mousePosition1.Y, cursorNowPosition.Z, 1);
                      
                viewProjInv.Invert();

                var clickedPosition = psCords * viewProjInv;
                clickedPosition /= clickedPosition.W;

                _scene.Cursor.PositionX = clickedPosition.X;
                _scene.Cursor.PositionY = clickedPosition.Y;
                _scene.Cursor.PositionZ = clickedPosition.Z;

                var currentUC = GetCurrentUserControl();
                if ( currentUC is MultiBezierCurveControl )
                {
                    //Add new point to BezierCurve
                    var bezierControl = currentUC as MultiBezierCurveControl;
                    if (bezierControl.AddPointOnClick == false) return;

                    var newPoint = new PointModel { Position = clickedPosition.Xyz };

                    _scene.ParametricModels.Add(newPoint);
                    bezierControl.AddPointToCurve(newPoint);
                    RefreshElementsListBox();
                    bezierControl.RefreshPointsListBox();
                }
                else if (currentUC is MultiBezierCurveC2Control)
                {
                    //Add new point to BezierCurveC2
                    var bezierControl = currentUC as MultiBezierCurveC2Control;
                    if (bezierControl.AddPointOnClick == false) return;

                    var newPoint = new PointModel {Position = clickedPosition.Xyz};

                    _scene.ParametricModels.Add(newPoint);
                    bezierControl.AddPointToCurve(newPoint);
                    RefreshElementsListBox();
                    bezierControl.RefreshPointsListBox();
                }
            }
        }

        private bool ArePointsNear2D(PointF point1, Vector4 point2)
        {
            var distance = Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
            if (distance < 0.1f) return true;
            return false;
        }
    }
}
