using System;
using System.Drawing;
using BLL.Shapes;
using Rectangle = System.Drawing.Rectangle;

namespace BLL.Drawing
{
    public class DrawSquare : DrawShape<Square>
    {
        protected override void OnDraw(Square square, Graphics g)
        {
            var p1 = square.Points[0];
            var p2 = square.Points[1];

            int miny = Math.Min(p1.Y, p2.Y),
                minx = Math.Min(p1.X, p2.X),
                maxy = Math.Max(p1.Y, p2.Y),
                maxx = Math.Max(p1.X, p2.X),
                width = Math.Min(maxx - minx, maxy - miny);
            g.FillRectangle(square.Brush, new Rectangle(minx, miny, width, width));
        }
    }
}