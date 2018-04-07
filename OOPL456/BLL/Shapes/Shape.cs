using System;
using System.Drawing;

namespace BLL.Shapes
{
    public abstract class Shape
    {
        protected Point[] PointsProtected;

        public virtual Point[] Points
        {
            get { return PointsProtected;}
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                if (value.Length != PointsCount)
                {
                    throw new ArgumentOutOfRangeException($"{Name} should have {PointsCount} points.");
                }
                PointsProtected = value;
            }
        }

        public virtual Pen Pen { get; set; }
        public virtual SolidBrush Brush { get; set; }
        public abstract int PointsCount { get; }
        public abstract string Name { get; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}