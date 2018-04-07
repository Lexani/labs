using System.Drawing;
using BLL.Drawing;

namespace PluginModule
{
    public class DrawStar : DrawShape<Star>
    {
        protected override void OnDraw(Star star, Graphics g)
        {
            var points = new []
            {
                star.Points[0],
                star.Points[2],
                star.Points[4],
                star.Points[1],
                star.Points[3],
            };
            g.FillPolygon(star.Brush, points);
        }
    }
}