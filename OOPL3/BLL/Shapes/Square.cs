using System;
using System.Drawing;

namespace BLL.Shapes
{
    public class Square : Shape
    {
        public override int PointsCount { get; } = 2; // top-left and bottom-right corners
        public override string Name { get; } = "Square";
        
        public Square()
        {
            PointsProtected = new Point[2];
        }
    }
}