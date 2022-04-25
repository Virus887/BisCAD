using BisCAD.Common;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;

namespace BisCAD.ParametricModels
{
    public class PointModel : ParametricModel
	{

		public float PointScale = 0.01f;

		public PointModel()
        {
            Name = $"Point {Global.PointsCount++}";
			Update();
        }

		//virtual model constructor
		public PointModel(Vector3 position, int id)
		{
			Name = $"Bezier Virtual Point {id}";
			Position = position;
			Update();
		}

		public override void Draw(Shader shader)
        {
            int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");

            if (IsSelected)
                GL.Uniform4(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);
            else
                GL.Uniform4(vertexColorLocation, Color.X, Color.Y, Color.Z, 1.0f);

            shader.SetMatrix4("model", Matrix4.Identity);
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        protected override void CreateVertices()
        {
			_vertices = new float[]
				{    // Front face
				-0.5f, -0.5f,  0.5f,
				0.5f,  -0.5f,  0.5f,
				0.5f,  0.5f,  0.5f,
				-0.5f,  0.5f,  0.5f,

				// Back face
				-0.5f, -0.5f, -0.5f,
				-0.5f,  0.5f, -0.5f,
				0.5f,  0.5f, -0.5f,
				0.5f, -0.5f, -0.5f,

				// Top face
				-0.5f,  0.5f, -0.5f,
				-0.5f,  0.5f,  0.5f,
				0.5f,  0.5f,  0.5f,
				0.5f,  0.5f, -0.5f,

				// Bottom face
				-0.5f, -0.5f, -0.5f,
				0.5f, -0.5f, -0.5f,
				0.5f, -0.5f,  0.5f,
				-0.5f, -0.5f,  0.5f,
		
				// Right face
				0.5f, -0.5f, -0.5f,
				0.5f,  0.5f, -0.5f,
				0.5f,  0.5f,  0.5f,
				0.5f, -0.5f,  0.5f,
		
				// Left face
				-0.5f, -0.5f, -0.5f,
				-0.5f, -0.5f,  0.5f,
				-0.5f,  0.5f,  0.5f,
				-0.5f,  0.5f, -0.5f
				};

			for(int i=0; i < _vertices.Length / 3; i++)
            {
				_vertices[3 * i] *= PointScale;
				_vertices[3 * i + 1] *= PointScale;
				_vertices[3 * i + 2] *= PointScale;

				_vertices[3 * i] += PositionX;
				_vertices[3 * i + 1] += PositionY;
				_vertices[3 * i + 2] += PositionZ;
            }

            _indices = new uint[]  
			{
				0, 1, 2, 0, 2, 3,         // front
				4, 5, 6, 4, 6, 7,         // back
				8, 9, 10, 8, 10, 11,      // top
				12, 13, 14, 12, 14, 15,   // bottom
				16, 17, 18, 16, 18, 19,   // right
				20, 21, 22, 20, 22, 23,   // left
			};
		}
    }
}
