using System.Drawing;
using OOPL12.BLL.DrawManagers;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.Creators
{
    public class TriangleDrawCreator : DrawCreator
    {
        public override IDrawManager Create(System.Drawing.Point[] arrayPoint)
        {
            Triangle triangle = new Triangle(arrayPoint[0], arrayPoint[1], arrayPoint[2], Color.Black, Color.Black);
            return new DrawTriangle(triangle);
        }
    }
}
