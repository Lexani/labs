using System.Drawing;

namespace OOPL12.BLL.Shapes
{
    public abstract class Shape
    {
        public abstract Point Point1 { get; set; }
        public abstract Point Point2 { get; set; }
        public virtual Brush Brush { get; set; }
        public abstract Pen Pen { get; set; }
    }
}
