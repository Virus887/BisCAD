using BisCAD.Common;
using OpenTK;

namespace BisCAD.ParametricModels
{
    public interface IParametricModel
    {
        string Name { get; set; }
        string DisplayName { get; }
        bool IsSelected { get; set; }

        Vector3 Color { get; set; }

        Vector3 Position { get; set; }

        float PositionX { get; set; }
        float PositionY { get; set; }
        float PositionZ { get; set; }

        float RotationX { get; set; }
        float RotationY { get; set; }
        float RotationZ { get; set; }

        float ScaleX { get; set; }
        float ScaleY { get; set; }
        float ScaleZ { get; set; }

        //Model got from rotations and scale ^
        Matrix4 CurrentModel { get; set; }

        //Previous combined model
        Matrix4 PrevModel { get; set; }

        //Model for tmp editing
        Matrix4 TmpModel { get; set; }


        void Draw(Shader _shader);
        void Dispose();
        void Update();
    }
}
