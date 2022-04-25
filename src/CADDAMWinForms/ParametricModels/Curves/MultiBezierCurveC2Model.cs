using BisCAD.Common;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Linq;

namespace BisCAD.ParametricModels.Curves
{
    public class MultiBezierCurveC2Model : BernsteinCurve
    {
        private List<IParametricModel> _deBoorePoints;
        public List<IParametricModel> DeBoorePoints
        {
            get
            {
                return _deBoorePoints;
            }
        }

        private bool showBernsteinPoints = false;
        public bool ShowBernsteinPoints
        {
            get { return showBernsteinPoints; }
            set
            {
                if (value == showBernsteinPoints) return;
                showBernsteinPoints = value;
                OnPropertyChanged("ShowBernsteinPoints");
                Update();
            }
        }

        private bool showDeBoorePolygon = false;
        public bool ShowDeBoorePolygon
        {
            get { return showDeBoorePolygon; }
            set
            {
                if (value == showDeBoorePolygon) return;
                showDeBoorePolygon = value;
                OnPropertyChanged("ShowDeBoorePolygon");
                Update();
            }
        }

        public override string DisplayName => Name;

        public  MultiBezierCurveC2Model(List<IParametricModel> points)
        {
            _deBoorePoints = points;
            _bernsteinPoints = CalculateBernsteinPoints(_deBoorePoints);

            name = $"Bezier Curve C2 {Global.BezierC2Count++}";
            Update();
        }

        //TODO: refactor
        //Takes deBoorePoints and creates berstein points for C2 Curve
        private List<IParametricModel> CalculateBernsteinPoints(List<IParametricModel> deBoorePoints)
        {

            if (deBoorePoints.Count < 4) return new List<IParametricModel>();

            int nowBernsteinPointsCount = (deBoorePoints.Count - 3) * 3 + 1;
            if(_bernsteinPoints == null || _bernsteinPoints.Count != nowBernsteinPointsCount)
            {
                var result = new List<IParametricModel>();

                //Add first point
                result.Add(new PointModel(deBoorePoints[0].Position, 0));

                for (int i = 0; i < deBoorePoints.Count - 1; i++)
                {
                    //Create 3 points
                    var deBoore1 = deBoorePoints[i];
                    var deBoore2 = deBoorePoints[i + 1];
                    Vector3 delta = (deBoore2.Position - deBoore1.Position) / 3;

                    PointModel p1 = new PointModel(deBoore1.Position + delta, i * 3 - 1);
                    PointModel p2 = new PointModel(deBoore1.Position + 2 * delta, i * 3);
                    PointModel p3 = new PointModel(deBoore1.Position + 3 * delta, i * 3 + 1);
                    result.Add(p1);
                    result.Add(p2);
                    result.Add(p3);
                }

                //zrób te poziome linie
                for (int i = 3; i < result.Count - 1; i += 3)
                {
                    result[i].Position = (result[i - 1].Position + result[i + 1].Position) / 2;
                }

                //usuń niepotrzebne wezły
                result = result.Skip(3).Reverse().Skip(3).Reverse().ToList();
                result.ForEach(point => point.Color = new Vector3(0.5f, 1.0f, 0.5f));
                result.ForEach(point => (point as PointModel).PointScale *= 0.8f);
                result.ForEach(point => point.Update());
                return result;
            }
            else
            {
                var positions = new List<Vector3>();

                positions.Add(deBoorePoints[0].Position);

                for (int i = 0; i < deBoorePoints.Count - 1; i++)
                {
                    //Create 3 points
                    var deBoore1 = deBoorePoints[i];
                    var deBoore2 = deBoorePoints[i + 1];
                    Vector3 delta = (deBoore2.Position - deBoore1.Position) / 3;

                    positions.Add(deBoore1.Position + delta);
                    positions.Add(deBoore1.Position + 2 * delta);
                    positions.Add(deBoore1.Position + 3 * delta);
                }

                for (int i = 3; i < positions.Count - 1; i += 3)
                {
                    positions[i] = (positions[i - 1] + positions[i + 1]) / 2;
                }

                positions = positions.Skip(3).Reverse().Skip(3).Reverse().ToList();
                for(int i = 0; i<positions.Count; i++)
                {
                    _bernsteinPoints[i].Position = positions[i];
                }
                _bernsteinPoints.ForEach(point => point.Update());
                return _bernsteinPoints;
            }


        }


  

        private float[] _polygonVertices;
        private uint[] _polygonIndices;

        private int _vertexBufferObjectDeBoorePolygon;
        private int _vertexArrayObjectDeBoorePolygon;
        private int _elementBufferObjectDeBoorePolygon;

        //TODO: Pewnie da się dużo prościej
        private void CreateDeBoorePolygonVerticesAndIndices()
        {

            if (_deBoorePoints.Count < 2)
            {
                _polygonVertices = new float[0];
                _polygonIndices = new uint[0];
                return;
            }

            int linesCount = _deBoorePoints.Count() - 1;
            int verticesPerCurve = 4;
            int indicesPerCurve = 3;

            int verticesCount = linesCount * verticesPerCurve;
            int indicesCount = linesCount * indicesPerCurve;

            _polygonVertices = new float[verticesCount * 3];
            _polygonIndices = new uint[indicesCount * 2];

            int curveNumber = 0;
            while (curveNumber < linesCount)
            {
                var fourPoints = _deBoorePoints.Skip(curveNumber * 3).Take(4).ToList();

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


        //Overrides:

        protected override void CreateVertices()
        {
            base.CreateVertices();
            CreateDeBoorePolygonVerticesAndIndices();
        }

        protected override void BindBuffers()
        {
            base.BindBuffers();
            _vertexBufferObjectDeBoorePolygon = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObjectDeBoorePolygon);
            GL.BufferData(BufferTarget.ArrayBuffer, _polygonVertices.Length * sizeof(float), _polygonVertices, BufferUsageHint.StaticDraw);
            _vertexArrayObjectDeBoorePolygon = GL.GenVertexArray();

            GL.BindVertexArray(_vertexArrayObjectDeBoorePolygon);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _elementBufferObjectDeBoorePolygon = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObjectDeBoorePolygon);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _polygonIndices.Length * sizeof(uint), _polygonIndices, BufferUsageHint.StaticDraw);
        }




        public override void Draw(Shader _shader)
        {
            base.Draw(_shader);

            //TODO: change colors 
            if (showBernsteinPoints)
            {
                foreach (var point in _bernsteinPoints)
                {
                    point.Draw(_shader);
                }
            }

            if (showDeBoorePolygon )
            {
                int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
                GL.Uniform4(vertexColorLocation, 0.8f, 0.4f, 0.4f, 1.0f);


                _shader.SetMatrix4("model", Transformation);
                GL.BindVertexArray(_vertexArrayObjectDeBoorePolygon);
                GL.DrawElements(PrimitiveType.Lines, _polygonIndices.Length, DrawElementsType.UnsignedInt, 0);
            }

        }

        public override void Dispose()
        {
            base.Dispose();
            GL.DeleteBuffer(_vertexBufferObjectDeBoorePolygon);
            GL.DeleteVertexArray(_vertexArrayObjectDeBoorePolygon);
        }

        public override void Update()
        {
            Dispose();
            _bernsteinPoints = CalculateBernsteinPoints(_deBoorePoints);
            CreateVertices();
            BindBuffers();

        }

    }
}
