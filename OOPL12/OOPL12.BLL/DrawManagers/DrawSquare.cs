using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawSquare : IDrawManager
    {
        private Square square;

        public DrawSquare(Square square)
        {
            this.square = square;
        }
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillRectangle(this.square.Brush, this.square.Point1.X, this.square.Point1.Y,
                this.square.Width, this.square.Width);
        }
    }
}