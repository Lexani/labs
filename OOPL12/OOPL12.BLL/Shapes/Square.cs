using System;
using System.Drawing;

namespace OOPL12.BLL.Shapes
{
    public class Square : Shape
    {
        private int width;
        private Point point1;
        private Point point2;
        private Brush brush;
        private Pen pen;

        public Square(Point point1, Point point2, Color brushColor, Color penColor)
        {
            width = Math.Abs(point1.X - point2.X);

            this.point1 = point1;
            this.point2 = point2;
            brush = new SolidBrush(brushColor);
            pen = new Pen(penColor);
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
        
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
    }
}
