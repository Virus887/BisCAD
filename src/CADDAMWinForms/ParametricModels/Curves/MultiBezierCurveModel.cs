using BisCAD.Common;
using BisCAD.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite;

namespace BisCAD.ParametricModels.Curves
{
    public class MultiBezierCurveModel : BernsteinCurve
    {
		public override string DisplayName => Name;
		public MultiBezierCurveModel(List<IParametricModel> points)
        {
			_bernsteinPoints = points;
			name = $"Bezier Curve {Global.BezierCount++}";

			Update();
		}



	}
}
