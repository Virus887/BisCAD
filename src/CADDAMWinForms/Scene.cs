using BisCAD.Common;
using BisCAD.ParametricModels;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BisCAD
{
    public class Scene
    {
        public List<IParametricModel> ParametricModels { get; }

        public IParametricModel Cursor { get; set; }


        public IParametricModel _platform;

        public Shader _shader;

        public GLControl _glControl;
        public CheckBox _showPlatformCheckbox;

        public Scene(GLControl glControl, CheckBox checkbox)
        {
            _showPlatformCheckbox = checkbox;
            _glControl = glControl;

            Global.WIDTH = _glControl.Width;
            Global.HEIGHT = _glControl.Height;
            Global.Camera = new Camera(Global.cameraStartPosition, _glControl.Width / glControl.Height);
            Global.Camera.Yaw = 0;

            ParametricModels = new List<IParametricModel>();
        }

        public void Load()
        {
            //Enable Z-buffer
            GL.Enable(EnableCap.DepthTest);

            //load shader
            _shader = new Shader("../../Shaders/vs.hlsl", "../../Shaders/fs.hlsl");
            Global.BernsteinShader = new BernsteinShader("../../Shaders/bernstein_vs.hlsl", "../../Shaders/bernstein_gs.hlsl", "../../Shaders/bernstein_fs.hlsl");
            _shader.Use();

            //load Models to draw
            //ParametricModels.Add(new TorusParametricModel());

            _platform = new PlatformModel();
            Cursor = new CursorModel();

            _glControl.Invalidate();
        }
        public void Render()
        {
            GL.ClearColor(0.3f, 0.2f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Set View and Projection Matrices
            _shader.SetMatrix4("view", Global.Camera.GetViewMatrix());
            _shader.SetMatrix4("projection", Global.Camera.GetProjectionMatrix());

            //draw objects
            foreach (var obj in ParametricModels) obj.Draw(_shader);

            if(_showPlatformCheckbox.Checked) _platform.Draw(_shader);
            Cursor.Draw(_shader);

            _glControl.SwapBuffers();
            _glControl.Invalidate();
        }

        public void Resize()
        {
            Global.WIDTH = _glControl.Width;
            Global.HEIGHT = _glControl.Height;
            GL.Viewport(0, 0, _glControl.Width, _glControl.Height);
            _glControl.Invalidate();
        }

        public void Dispose()
        {
            foreach (var obj in ParametricModels)
                obj.Dispose();
        }
    }
}
