using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawEllipse : IDrawManager
    {
        private Ellipse ellipse;

        public DrawEllipse(Ellipse ellipse)
        {
            this.ellipse = ellipse;
        }
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillEllipse(this.ellipse.Brush, this.ellipse.Point1.X, this.ellipse.Point1.Y,
                this.ellipse.Length, this.ellipse.Width);
        }
    }
}
