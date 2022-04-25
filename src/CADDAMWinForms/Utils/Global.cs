using BisCAD.Common;
using BisCAD.ParametricModels;
using OpenTK;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BisCAD.Utils
{
    public static class Global
    {
        public static float TorusR1 = 0.8f;
        public static int TorusR1Partitions = 10;
        public static float TorusR2 = 0.3f;
        public static int TorusR2Partitions = 10;

        public static int WIDTH = 800;
        public static int HEIGHT = 1000;


        public static Stopwatch Timer = new Stopwatch();

        public static int TorusCount = 0;
        public static int PointsCount = 0;
        public static int BezierCount = 0;
        public static int BezierC2Count = 0;

        public static MainForm mainForm;

        public static Vector3 cameraStartPosition = new Vector3(-1, 0, 0);
        public static Camera Camera;
        public static BernsteinShader BernsteinShader;
    }
}
