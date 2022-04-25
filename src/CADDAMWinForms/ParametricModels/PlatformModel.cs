using BisCAD.Common;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace BisCAD.ParametricModels
{
    public class PlatformModel : ParametricModel
	{
		private int partitions = 30;

		private (float min, float max) alfa = (-5, 5);
		private (float min, float max) beta = (-5, 5);

		public string Name { get; set; }

		public PlatformModel()
		{
			CreateVertices();
			BindBuffers();
		}


		protected override void CreateVertices()
		{
			_vertices = new float[partitions * partitions * 3];
			_indices = new uint[partitions * partitions * 4];

			for (int i = 0; i < partitions; i++)
			{
				float tmpAlfa = alfa.min + i * ((alfa.max - alfa.min) / partitions);
				for (int j = 0; j < partitions; j++)
				{
					float tmpBeta = beta.min + j * ((beta.max - beta.min) / partitions);
					//Asign X
					_vertices[(i * partitions + j) * 3] = tmpAlfa;
					//Asingn Y
					_vertices[(i * partitions + j) * 3 + 1] = -1;
					//Asign Z
					_vertices[(i * partitions + j) * 3 + 2] = tmpBeta;


					//line 1
					_indices[(i * partitions + j) * 4] = (uint)(i * partitions + j);

					//line 2
					_indices[(i * partitions + j) * 4 + 1] = (uint)(i * partitions + (j + 1) % partitions);

					//line 3
					_indices[(i * partitions + j) * 4 + 2] = (uint)(i * partitions + j);

					//line 4
					_indices[(i * partitions + j) * 4 + 3] = (uint)(((i + 1) % partitions) * partitions + j);
				}
			}
		}



		public override void Draw(Shader shader)
		{
			int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
			GL.Uniform4(vertexColorLocation, 0.0f, 0.0f , 0.0f, 1.0f);

			shader.SetMatrix4("model", Transformation);
			GL.BindVertexArray(_vertexArrayObject);
			GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);
		}
    }
}
