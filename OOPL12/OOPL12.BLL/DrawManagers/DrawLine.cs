using System.Drawing;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawLine : IDrawManager
    {
        private Line line;
        
        public DrawLine(Line line)
        {
            this.line = line;
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(this.line.Pen, this.line.Point1, this.line.Point2);
        }
    }
}
