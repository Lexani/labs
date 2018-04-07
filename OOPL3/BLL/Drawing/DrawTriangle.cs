using System.Drawing;
using BLL.Shapes;

namespace BLL.Drawing
{
    public class DrawTriangle : DrawShape<Triangle>
    {
        protected override void OnDraw(Triangle triangle, Graphics g)
        {
            g.FillPolygon(triangle.Brush, triangle.Points);
        }
    }
}