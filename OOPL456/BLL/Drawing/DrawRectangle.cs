using System;
using System.Drawing;
using BLL.Shapes;
using Rectangle = BLL.Shapes.Rectangle;

namespace BLL.Drawing
{
    public class DrawRectangle : DrawShape<BLL.Shapes.Rectangle>
    {
        protected override void OnDraw(Rectangle rectangle, Graphics g)
        {
            var p1 = rectangle.Points[0];
            var p2 = rectangle.Points[1];

            int miny = Math.Min(p1.Y, p2.Y),
                minx = Math.Min(p1.X, p2.X),
                maxy = Math.Max(p1.Y, p2.Y),
                maxx = Math.Max(p1.X, p2.X),
                width = maxx - minx,
                height = maxy - miny;
            g.FillRectangle(rectangle.Brush, new System.Drawing.Rectangle(minx, miny, width, height));
        }
        
         
    }
}