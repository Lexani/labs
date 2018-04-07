using System.Drawing;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.DrawManagers
{
    public class DrawTriangle : IDrawManager
    {
        private Triangle triangle;

        public DrawTriangle(Triangle triangle)
        {
            this.triangle = triangle;
        }
        public void Draw(System.Drawing.Graphics g)
        {
            g.FillPolygon(this.triangle.Brush, new Point[]{this.triangle.Point1, this.triangle.Point2,
                this.triangle.Point3});
        }
    }
}
