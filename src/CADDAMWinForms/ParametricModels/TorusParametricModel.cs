using BisCAD.Common;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace BisCAD.ParametricModels
{
	public class TorusParametricModel : ParametricModel
	{
		//Consts
		private (float min, float max) alfa = (0, 2 * MathHelper.Pi);
		private (float min, float max) beta = (0, 2 * MathHelper.Pi);


		private float r1;
		private float r2;
		private int partitions1;
		private int partitions2;

		#region Properties
		public float R1
        {
            get { return r1; }
            set
            {
				if (value == r1) return;
				r1 = value;
				OnPropertyChanged("R1");
				CreateVertices();
				BindBuffers();
			}
        }

		public float R2
		{
			get { return r2;}
			set
			{
				if (value == r2) return;
				r2 = value;
				OnPropertyChanged("R2");
				CreateVertices();
				BindBuffers();
			}
		}

		public int Partitions1
		{
			get { return partitions1; }
			set
			{
				if (value == partitions1) return;
				partitions1 = value;
				OnPropertyChanged("Partitions1");
				CreateVertices();
				BindBuffers();
			}
		}

		public int Partitions2
		{
			get { return partitions2; }
			set
			{
				if (value == partitions2) return;
				partitions2 = value;
				OnPropertyChanged("Partitions2");
				CreateVertices();
				BindBuffers();
			}
		}
		#endregion

		public TorusParametricModel(float r1, float r2, int partitions1, int partitions2)
        {
			this.r1 = r1;
			this.r2 = r2;
			this.partitions1 = partitions1;
			this.partitions2 = partitions2;
			Name = $"Torus {Global.TorusCount++}";

			CreateVertices();
			BindBuffers();
		}

		public TorusParametricModel()
		{
			r1 = 0.8f;
			r2 = 0.3f;
			partitions1 = 20;
			partitions2 = 20;

			CreateVertices();
			BindBuffers();
		}


		protected override void CreateVertices()
        {
			_vertices = new float[partitions1 * partitions2 * 3];
			_indices = new uint[partitions1 * partitions2 * 4];
		
			for(int i=0; i<partitions1; i++)
            {
				float tmpAlfa = alfa.min + i * ((alfa.max - alfa.min) / partitions1);
				for (int j=0; j<partitions2; j++)
                {
					float tmpBeta = beta.min + j * ((beta.max - beta.min) / partitions2);
					//Asign X
					_vertices[(i * partitions2 + j) * 3] = (R1 + R2 * (float)Math.Cos(tmpBeta)) * (float)Math.Cos(tmpAlfa);
					//Asingn Y
					_vertices[(i * partitions2 + j) * 3 + 1] = (R1 + R2 * (float)Math.Cos(tmpBeta)) * (float)Math.Sin(tmpAlfa);
					//Asign Z
					_vertices[(i * partitions2 + j) * 3 + 2] = R2 * (float)Math.Sin(tmpBeta);


					//line 1
					_indices[(i * partitions2 + j) * 4] = (uint)(i * partitions2 + j);

					//line 2
					_indices[(i * partitions2 + j) * 4 + 1] = (uint)(i * partitions2 + (j + 1) % partitions2);

					//line 3
					_indices[(i * partitions2 + j) * 4 + 2] = (uint)(i * partitions2 + j);

					//line 4
					_indices[(i * partitions2 + j) * 4 + 3] = (uint)(((i + 1) % partitions1) * partitions2 + j);
				}
            }
        }


		public override void Draw(Shader shader)
        {
			Matrix4 Transformation =
				 PrevModel * CurrentModel;

			//set painted color
			double timeValue = Global.Timer.Elapsed.TotalSeconds;
			float greenValue = (float)Math.Sin(timeValue) / (2.0f + 0.5f);
			int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");

			if(IsSelected)
				GL.Uniform4(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);
			else
				GL.Uniform4(vertexColorLocation, 1.0f, greenValue, 0.0f, 1.0f);

			//draw
			shader.SetMatrix4("model", Transformation);
			GL.BindVertexArray(_vertexArrayObject);
			GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);
		}

    }
}
