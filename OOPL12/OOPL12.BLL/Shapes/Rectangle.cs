using System;
using System.Drawing;

namespace OOPL12.BLL.Shapes
{
    public class Rectangle : Shape
    {
        private int length;
        private int width;
        private Point point1;
        private Point point2;
        private Brush brush;
        private Pen pen;
        
        public Rectangle(Point point1, Point point2, Color brushColor, Color penColor)
        {
            this.point1 = point1;
            this.point2 = point2;
            brush = new SolidBrush(brushColor);
            pen = new Pen(penColor);
            length = Math.Abs(point1.X - point2.X);
            width = Math.Abs(point1.Y - point2.Y);
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

        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}
