using System.Drawing;
using BLL.Shapes;

namespace StarPlugin
{
    public class Star : Shape
    {
        public override int PointsCount { get; } = 5;
        public override string Name { get; } = "Star";

        public Star()
        {
            PointsProtected = new Point[5];
        }
    }
}
