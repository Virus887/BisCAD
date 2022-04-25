using BisCAD.Common;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisCAD.ParametricModels.Curves
{
    public class BernsteinCurve : ParametricModel
	{
		//TODO: _points -> Points
		protected List<IParametricModel> _bernsteinPoints;
		public List<IParametricModel> BernsteinPoints 
		{
            get
            {
				return _bernsteinPoints;
            }
		}

		private (float min, float max) domain1 = (0, 1);

		//TODO: showBernsteinPolyyygon
		protected bool showBernsteinPolygon = false;

		public string Partitions => string.Join("\n", CurvePartitions);

		public bool ShowBernsteinPolygon
		{
			get { return showBernsteinPolygon; }
			set
			{
				if (value == showBernsteinPolygon) return;
				showBernsteinPolygon = value;
				OnPropertyChanged("ShowBernsteinPolygon");
				Update();
			}
		}

		protected override void CreateVertices()
		{
			SetPartitions();
			CreatePolygonVerticesAndIndices();
			CalculateBezierCoeffs();


			//every curve has partitions + 1 vertices
			int allVerticesCount = (CurvePartitions.Sum() + 1) * CurvePartitions.Count;
			int allIndicesCount = CurvePartitions.Sum();

			_vertices = new float[allVerticesCount * 3];
			_indices = new uint[allIndicesCount * 2];


			//TODO: ucina ostatną krawędź
			int curveNumber = 0;
			int partitionsIterated = 0;
			while (curveNumber < CurvePartitions.Count)
			{
				//int verticesPerCurve = (partitions + 1) * 3;
				//int indicesPerCurve = 2 * partitions;

				var fourPoints = BernsteinPoints.Skip(curveNumber * 3).Take(4).ToList();

				for (int i = 0; i < CurvePartitions[curveNumber] + 1; i++)
				{
					Vector3 curvePosition = Vector3.Zero;
					switch (fourPoints.Count)
					{
						case 4:
							curvePosition = fourPoints[0].Position * B3[curveNumber][i].X
											+ fourPoints[1].Position * B3[curveNumber][i].Y
											+ fourPoints[2].Position * B3[curveNumber][i].Z
											+ fourPoints[3].Position * B3[curveNumber][i].W;
							break;
						case 3:
							curvePosition = fourPoints[0].Position * B2[curveNumber][i].X
											+ fourPoints[1].Position * B2[curveNumber][i].Y
											+ fourPoints[2].Position * B2[curveNumber][i].Z;
							break;
						case 2:
							curvePosition = fourPoints[0].Position * B1[curveNumber][i].X
											+ fourPoints[1].Position * B1[curveNumber][i].Y;
							break;
						default:
							break;
					}

					_vertices[3 * (i + partitionsIterated + curveNumber)] = curvePosition.X;
					_vertices[3 * (i + partitionsIterated + curveNumber) + 1] = curvePosition.Y;
					_vertices[3 * (i + partitionsIterated + curveNumber) + 2] = curvePosition.Z;

					if (i < CurvePartitions[curveNumber])
					{
						_indices[2 * (i + partitionsIterated)] = (uint)(i + partitionsIterated + curveNumber);
						_indices[2 * (i + partitionsIterated) + 1] = (uint)(i + partitionsIterated + curveNumber + 1);
					}

				}
				partitionsIterated += CurvePartitions[curveNumber++];
			}
		}

		//TODO Create on property changed to show it when it changes
		//Change name to say more about property
		private List<int> CurvePartitions;

		private void SetPartitions()
		{
			CurvePartitions = new List<int>();

			int curvesCount = _bernsteinPoints.Count > 1 ? 1 : 0;
			if (_bernsteinPoints.Count > 4)
			{
				//curves n=3
				curvesCount += ((_bernsteinPoints.Count - 4) / 3);
				//lastCurve
				curvesCount += (_bernsteinPoints.Count - 4) % 3 != 0 ? 1 : 0;
			}
			int curveNumber = 0;
			while (curveNumber < curvesCount)
			{
				var fourPoints = _bernsteinPoints.Skip(curveNumber * 3).Take(4).ToList();

				List<Vector2> pointsOnScreen = new List<Vector2>();
				foreach (var point in fourPoints)
				{
					var pointOnScreen = new Vector4(point.Position, 1) * (Global.Camera.GetViewMatrix() * Global.Camera.GetProjectionMatrix());
					pointOnScreen /= pointOnScreen.W;
					pointsOnScreen.Add(pointOnScreen.Xy);
				}

				float areaMultiplyer = Global.WIDTH * Global.HEIGHT / 1000.0f;

				float area = Area(pointsOnScreen);
				int polygonArea = (int)(area * areaMultiplyer) + 1;

				if (polygonArea > 2000 || polygonArea < 0)
					polygonArea = 2000;

				CurvePartitions.Add(polygonArea);

				curveNumber++;
			}

		}

		public float Area(List<Vector2> vertices)
		{
			if (vertices.Count <= 2) return 0;

			if (vertices.All(point => (point.X < -1 || point.X > 1) && (point.Y < -1 || point.Y > 1)))
				return 50;

			if (vertices.Count == 3)
			{
				return TriangleArea(vertices[0], vertices[1], vertices[2]);
			}

			if (vertices.Count == 4)
			{
				float minXPoint = vertices.OrderBy(x => x.X).First().X;
				float maxXPoint = vertices.OrderBy(x => x.X).Last().X;
				float minYPoint = vertices.OrderBy(x => x.Y).First().Y;
				float maxYPoint = vertices.OrderBy(x => x.Y).Last().Y;
				return (float)Math.Abs((maxXPoint - minXPoint) * (maxYPoint - minYPoint));
			}
			return 0;

		}


		private float TriangleArea(Vector2 x, Vector2 y, Vector2 z)
		{
			float a = Distance(x, y);
			float b = Distance(y, z);
			float c = Distance(x, z);
			var p = (a + b + c) / 2;
			return (float)Math.Sqrt(p * (p - a) * (p - b) * (p - c));
		}

		private float Distance(Vector2 a, Vector2 b)
		{
			return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
		}


		#region git
		private float[] _polygonVertices;
		private uint[] _polygonIndices;

		private int _vertexBufferObjectBernsteinPolygon;
		private int _vertexArrayObjectBernsteinPolygon;
		private int _elementBufferObjectBernsteinPolygon;
		private void CreatePolygonVerticesAndIndices()
		{
			int curvesCount = CurvePartitions.Count();
			int verticesPerCurve = 4;
			int indicesPerCurve = 3;

			int verticesCount = curvesCount * verticesPerCurve;
			int indicesCount = curvesCount * indicesPerCurve;

			_polygonVertices = new float[verticesCount * 3];
			_polygonIndices = new uint[indicesCount * 2];

			int curveNumber = 0;
			while (curveNumber < curvesCount)
			{
				var fourPoints = _bernsteinPoints.Skip(curveNumber * 3).Take(4).ToList();

				for (int i = 0; i < fourPoints.Count; i++)
				{
					//Asingn X
					_polygonVertices[3 * (i + curveNumber * verticesPerCurve)] = fourPoints[i].PositionX;
					//Asingn Y
					_polygonVertices[3 * (i + curveNumber * verticesPerCurve) + 1] = fourPoints[i].PositionY;
					//Asign Z
					_polygonVertices[3 * (i + curveNumber * verticesPerCurve) + 2] = fourPoints[i].PositionZ;


					if (i < fourPoints.Count - 1)
					{
						_polygonIndices[2 * (i + curveNumber * indicesPerCurve)] = (uint)(i + curveNumber * verticesPerCurve);
						_polygonIndices[2 * (i + curveNumber * indicesPerCurve) + 1] = (uint)(i + 1 + curveNumber * verticesPerCurve);
					}
				}

				curveNumber++;
			}

		}

		#endregion


		//TODO: Create normals using deCasteljeau, useful for debugging
		//private float[] _normalsVertices;
		//private float[] _normalsIndices;



		//---- Overrides ----

		protected override void BindBuffers()
		{
			base.BindBuffers();
			_vertexBufferObjectBernsteinPolygon = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectBernsteinPolygon);
			GL.BufferData(BufferTarget.ArrayBuffer, _polygonVertices.Length * sizeof(float), _polygonVertices, BufferUsageHint.StaticDraw);
			_vertexArrayObjectBernsteinPolygon = GL.GenVertexArray();

			GL.BindVertexArray(_vertexArrayObjectBernsteinPolygon);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);

			_elementBufferObjectBernsteinPolygon = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectBernsteinPolygon);
			GL.BufferData(BufferTarget.ElementArrayBuffer, _polygonIndices.Length * sizeof(uint), _polygonIndices, BufferUsageHint.StaticDraw);
		}

		public override void Dispose()
		{
			base.Dispose();
			GL.DeleteBuffer(_vertexBufferObjectBernsteinPolygon);
			GL.DeleteVertexArray(_vertexArrayObjectBernsteinPolygon);
		}


		// ----- DRAWING -----
		public override void Draw(Shader _shader)
		{
			Matrix4 Transformation = Matrix4.Identity;

			//set painted color
			double timeValue = Global.Timer.Elapsed.TotalSeconds;
			float greenValue = (float)Math.Sin(timeValue) / (2.0f + 0.5f);
			int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");

			if (IsSelected)
				GL.Uniform4(vertexColorLocation, 1.0f, 1.0f, 1.0f, 1.0f);
			else
				GL.Uniform4(vertexColorLocation, 1.0f, greenValue, 0.0f, 1.0f);

			//draw
			_shader.SetMatrix4("model", Transformation);
			GL.BindVertexArray(_vertexArrayObject);
			GL.DrawElements(PrimitiveType.Lines, _indices.Length, DrawElementsType.UnsignedInt, 0);


			//draw polygon
			if (showBernsteinPolygon && IsSelected)
			{
				vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
				GL.Uniform4(vertexColorLocation, 0.4f, 0.4f, 0.4f, 1.0f);


				_shader.SetMatrix4("model", Transformation);
				GL.BindVertexArray(_vertexArrayObjectBernsteinPolygon);
				GL.DrawElements(PrimitiveType.Lines, _polygonIndices.Length, DrawElementsType.UnsignedInt, 0);
			}
		}



		//PRIVATE METHODS:

		private Vector4[][] B3;
		private Vector3[][] B2;
		private Vector2[][] B1;

		// TODO: This one have to caltulate for every bezier curve
		private void CalculateBezierCoeffs()
		{
			try
			{
				B3 = new Vector4[CurvePartitions.Count][];
				B2 = new Vector3[CurvePartitions.Count][];
				B1 = new Vector2[CurvePartitions.Count][];
				for (int curveNumber = 0; curveNumber < CurvePartitions.Count; curveNumber++)
				{
					int vertCount = CurvePartitions[curveNumber] + 1; //plus 1!
					B3[curveNumber] = new Vector4[vertCount];
					B2[curveNumber] = new Vector3[vertCount];
					B1[curveNumber] = new Vector2[vertCount];
					for (int i = 0; i < vertCount; i++)
					{
						float tmpT = (domain1.max - domain1.min) / (vertCount - 1);
						CalculateBezierB1CoeffsforT(tmpT * i, i, curveNumber);
					}
				}
			}
			catch (Exception ex)
			{

			}
		}


		private void CalculateBezierB1CoeffsforT(float t, int idx, int curveNumber)
		{
			var tmpArray = new float[4, 4];
			tmpArray[0, 0] = 1;
			for (int i = 1; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					tmpArray[i, j] += tmpArray[i - 1, j] * (1 - t);
					if (j < 3) tmpArray[i, j + 1] += tmpArray[i - 1, j] * t;
				}
			}
			B3[curveNumber][idx] = new Vector4(tmpArray[3, 0], tmpArray[3, 1], tmpArray[3, 2], tmpArray[3, 3]);
			B2[curveNumber][idx] = new Vector3(tmpArray[2, 0], tmpArray[2, 1], tmpArray[2, 2]);
			B1[curveNumber][idx] = new Vector2(tmpArray[1, 0], tmpArray[1, 1]);
		}
	}
}
