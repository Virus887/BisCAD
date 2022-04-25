using BisCAD.Common;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;

namespace BisCAD.ParametricModels
{
    public abstract class ParametricModel : IParametricModel, INotifyPropertyChanged
    {
        public Matrix4 Transformation { get; set; } = Matrix4.Identity;
        public Vector3 Color { get; set; } = new Vector3(1, 0.5f, 0);

        protected string name;
        protected float positionX = 0;
        protected float positionY = 0;
        protected float positionZ = 0;

        protected float rotationX = 0;
        protected float rotationY = 0;
        protected float rotationZ = 0;

        protected float scaleX = 1;
        protected float scaleY = 1;
        protected float scaleZ = 1;

        #region Properties
        public bool IsSelected { get; set; }
        public Matrix4 CurrentModel
        { 
            get
            {
                return 
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(RotationZ)) *
                Matrix4.CreateRotationY(MathHelper.DegreesToRadians(RotationY)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(RotationX)) *
                Matrix4.CreateScale(ScaleX, ScaleY, ScaleZ) *
                Matrix4.CreateTranslation(positionX, positionY, positionZ);
            }
            set
            {
                CurrentModel = value;
            }
        }

        public Matrix4 PrevModel { get; set; } = Matrix4.Identity;

        public Matrix4 TmpModel { get; set; } = Matrix4.Identity;




        public virtual string Name
        {
            get { return name; }
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged("Name");
                Update();
            }
        }

        public virtual string DisplayName
        {
            get { return name + string.Format(" (X:{0:0.##}, Y:{1:0.##}, Z:{2:0.##} )", positionX, positionY, positionZ); }
        }

        public virtual Vector3 Position
        {
            get
            {
                return new Vector3(positionX, positionY, positionZ);
            }
            set
            {
                positionX = value.X;
                positionY = value.Y;
                positionZ = value.Z;
            }
        }
            
        public virtual float PositionX
        {
            get { 
                return positionX; 
            }
            set
            {
                if (value == positionX) return;
                positionX = value;
                OnPropertyChanged("PositionX");
                Update();
            }
        }       
        
        public virtual float PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                if (value == positionY) return;
                positionY = value;
                OnPropertyChanged("PositionY");
                Update();
            }
        }        
        
        public virtual float PositionZ
        {
            get { return positionZ; }
            set
            {
                if (value == positionZ) return;
                positionZ = value;
                OnPropertyChanged("PositionZ");
                Update();
            }
        }


        public virtual float RotationX
        {
            get { return rotationX; }
            set
            {
                if (value == rotationX) return;
                rotationX = value;
                OnPropertyChanged("RotationX");
                Update();
            }
        }

        public virtual float RotationY
        {
            get { return rotationY; }
            set
            {
                if (value == rotationY) return;
                rotationY = value;
                OnPropertyChanged("RotationY");
                Update();
            }
        }

        public virtual float RotationZ
        {
            get { return rotationZ; }
            set
            {
                if (value == rotationZ) return;
                rotationZ = value;
                OnPropertyChanged("RotationZ");
                Update();
            }
        }

        public virtual float ScaleX
        {
            get { return scaleX; }
            set
            {
                if (value == scaleX) return;
                scaleX = value;
                OnPropertyChanged("ScaleX");
                Update();
            }
        }

        public virtual float ScaleY
        {
            get { return scaleY; }
            set
            {
                if (value == scaleY) return;
                scaleY = value;
                OnPropertyChanged("ScaleY");
                Update();
            }
        }

        public virtual float ScaleZ
        {
            get { return scaleZ; }
            set
            {
                if (value == scaleZ) return;
                scaleZ = value;
                OnPropertyChanged("ScaleZ");
                Update();
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        protected float[] _vertices;
        protected uint[] _indices;

        protected int _vertexBufferObject;
        protected int _vertexArrayObject;
        protected int _elementBufferObject;

        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void BindBuffers()
        {
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
        }

        public virtual void Dispose()
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
        }


        protected abstract void CreateVertices();
        public abstract void Draw(Shader _shader);

        public virtual void Update()
        {
            Dispose();
            CreateVertices();
            BindBuffers();
        }
    }
}
