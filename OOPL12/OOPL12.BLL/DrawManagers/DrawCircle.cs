using System.Drawing;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawCircle : IDrawManager
    {
        private Circle circle;

        public DrawCircle(Circle circle)
        {
            this.circle = circle;
        }
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillEllipse(this.circle.Brush, this.circle.Point1.X, this.circle.Point1.Y,
                this.circle.Width, this.circle.Width);
        }
    }
}