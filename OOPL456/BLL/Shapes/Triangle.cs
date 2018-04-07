using System.Drawing;

namespace BLL.Shapes
{
    public class Triangle : Shape
    {
        public override int PointsCount { get; } = 3;
        public override string Name { get; } = "Triangle";

        public Triangle()
        {
            PointsProtected = new Point[3];
        }
    }
}