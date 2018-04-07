using System.Drawing;

namespace OOPL12.BLL.Shapes
{
    public class Line : Shape
    {
        protected Point point1;
        protected Point point2;
        protected Pen pen;
        
        public Line(Point point1, Point point2, Color penColor)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.pen = new Pen(penColor);
        }

        public Line() { }

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
