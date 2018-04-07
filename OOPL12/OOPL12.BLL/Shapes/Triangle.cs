using System.Drawing;

namespace OOPL12.BLL.Shapes
{
    public class Triangle : Shape
    {
        private Point point1;
        private Point point2;
        private Point point3;
        private Brush brush;
        private Pen pen;
        

        public Triangle(Point point1, Point point2, Point point3, Color brushColor, Color penColor)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            this.brush = new SolidBrush(brushColor);
            this.pen = new Pen(penColor);
        }

        public override Point Point1
        {
            get
            {
                return point1;
            }
            set
            {
                point1 = value;
            }
        }

        public override Point Point2
        {
            get
            {
                return point2;
            }
            set
            {
                point2 = value;
            }
        }

        public Point Point3
        {
            get { return point3; }
            set { point3 = value; }
        }

        public override Brush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                brush = value;
            }
        }

        public override Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                pen = value;
            }
        }
    }
}
