using BisCAD.Common;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisCAD.ParametricModels
{
	public class CursorModel : ParametricModel
	{
		public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float RotationX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float RotationY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float RotationZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float ScaleX { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float ScaleY { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public float ScaleZ { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


		protected override void CreateVertices()
		{
			float cursorSize = 0.1f;
			float arrowSize = cursorSize / 20.0f;


			_verticesX = new float[]
			{
				positionX, positionY, positionZ,
				positionX + cursorSize, positionY, positionZ,
				positionX + cursorSize - 5*arrowSize, positionY + arrowSize, positionZ - arrowSize,
				positionX + cursorSize - 5*arrowSize, positionY + arrowSize, positionZ + arrowSize,
				positionX + cursorSize - 5*arrowSize, positionY - arrowSize, positionZ - arrowSize,
				positionX + cursorSize - 5*arrowSize, positionY - arrowSize, positionZ + arrowSize,
			};

			_verticesY = new float[]
			{
				positionX, positionY, positionZ,
				positionX, positionY + cursorSize, positionZ,
				positionX - arrowSize, positionY + cursorSize - 5*arrowSize, positionZ - arrowSize,
				positionX - arrowSize, positionY + cursorSize - 5*arrowSize, positionZ + arrowSize,
				positionX + arrowSize, positionY + cursorSize - 5*arrowSize, positionZ - arrowSize,
				positionX + arrowSize, positionY + cursorSize - 5*arrowSize, positionZ + arrowSize,
			};

			_verticesZ = new float[]
			{
				positionX, positionY, positionZ,
				positionX, positionY, positionZ + cursorSize,
				positionX - arrowSize, positionY + arrowSize, positionZ + cursorSize - 5*arrowSize,
				positionX - arrowSize, positionY - arrowSize, positionZ + cursorSize - 5*arrowSize,
				positionX + arrowSize, positionY + arrowSize, positionZ + cursorSize - 5*arrowSize,
				positionX + arrowSize, positionY - arrowSize, positionZ + cursorSize - 5*arrowSize,
			};

			_indicesX = new uint[] { 0, 1, 1, 2, 1, 3, 1, 4};
			_indicesY = new uint[] { 0, 1, 1, 2, 1, 3, 1, 4};
			_indicesZ = new uint[] { 0, 1, 1, 2, 1, 3, 1, 4};
		}

		public CursorModel()
		{
			CreateVertices();
			BindBuffers();
		}


		private float[] _verticesX;
		private float[] _verticesY;
		private float[] _verticesZ;

		private uint[] _indicesX;
		private uint[] _indicesY;
		private uint[] _indicesZ;


		private int _vertexBufferObjectX;
		private int _vertexArrayObjectX;
		private int _elementBufferObjectX;

		private int _vertexBufferObjectY;
		private int _vertexArrayObjectY;
		private int _elementBufferObjectY;

		private int _vertexBufferObjectZ;
		private int _vertexArrayObjectZ;
		private int _elementBufferObjectZ;


		protected override void BindBuffers()
		{
			//Bind X
			_vertexBufferObjectX = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectX);
			GL.BufferData(BufferTarget.ArrayBuffer, _verticesX.Length * sizeof(float), _verticesX, BufferUsageHint.StaticDraw);
			_vertexArrayObjectX = GL.GenVertexArray();

			GL.BindVertexArray(_vertexArrayObjectX);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			_elementBufferObjectX = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectX);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesX.Length * sizeof(uint), _indicesX, BufferUsageHint.StaticDraw);



			//BindY
			_vertexBufferObjectY = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectY);
			GL.BufferData(BufferTarget.ArrayBuffer, _verticesY.Length * sizeof(float), _verticesY, BufferUsageHint.StaticDraw);
			_vertexArrayObjectY = GL.GenVertexArray();

			GL.BindVertexArray(_vertexArrayObjectY);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			_elementBufferObjectY = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectY);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesY.Length * sizeof(uint), _indicesY, BufferUsageHint.StaticDraw);



			//BindZ

			_vertexBufferObjectZ = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectZ);
			GL.BufferData(BufferTarget.ArrayBuffer, _verticesZ.Length * sizeof(float), _verticesZ, BufferUsageHint.StaticDraw);
			_vertexArrayObjectZ = GL.GenVertexArray();

			GL.BindVertexArray(_vertexArrayObjectZ);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			_elementBufferObjectZ = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectZ);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _indicesZ.Length * sizeof(uint), _indicesZ, BufferUsageHint.StaticDraw);

		}

		public override void Dispose()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindVertexArray(0);
			GL.UseProgram(0);

			// Delete all the resources.
			GL.DeleteBuffer(_vertexBufferObjectX);
			GL.DeleteVertexArray(_vertexArrayObjectX);
			GL.DeleteBuffer(_vertexBufferObjectY);
			GL.DeleteVertexArray(_vertexArrayObjectY);
			GL.DeleteBuffer(_vertexBufferObjectZ);
			GL.DeleteVertexArray(_vertexArrayObjectZ);
		}



		public override void Draw(Shader shader)
		{
			int vertexColorLocation = GL.GetUniformLocation(shader.Handle, "ourColor");
			
			//draw
			shader.SetMatrix4("model", Matrix4.Identity);
            GL.BindVertexArray(_vertexArrayObjectX);
			GL.Uniform4(vertexColorLocation, 1.0f, 0.0f, 0.0f, 1.0f);
			GL.DrawElements(PrimitiveType.Lines, _indicesX.Length, DrawElementsType.UnsignedInt, 0);

			GL.BindVertexArray(_vertexArrayObjectY);
			GL.Uniform4(vertexColorLocation, 0.0f, 1.0f, 0.0f, 1.0f);
			GL.DrawElements(PrimitiveType.Lines, _indicesY.Length, DrawElementsType.UnsignedInt, 0);
			
			GL.BindVertexArray(_vertexArrayObjectZ);
			GL.Uniform4(vertexColorLocation, 0.0f, 0.0f, 1.0f, 1.0f);
			GL.DrawElements(PrimitiveType.Lines, _indicesZ.Length, DrawElementsType.UnsignedInt, 0);

		}

	}
}
