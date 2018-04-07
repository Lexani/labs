using System.Drawing;

namespace BLL.Shapes
{
    public class Rectangle : Shape
    {
        public override int PointsCount { get; } = 2;
        public override string Name { get; } = "Rectangle";

        public Rectangle()
        {
            PointsProtected = new Point[2];
        }
    }
}