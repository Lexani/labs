using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawRectangle : IDrawManager
    {
        private Rectangle rectangle;

        public DrawRectangle(Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(this.rectangle.Brush, this.rectangle.Point1.X, this.rectangle.Point1.Y,
                this.rectangle.Length, this.rectangle.Width);
        }
    }
}
