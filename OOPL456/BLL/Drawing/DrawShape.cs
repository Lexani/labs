using System.Drawing;
using BLL.Shapes;

namespace BLL.Drawing
{
    public abstract class DrawShape<T> : IDrawer where T : Shape, new()
    {
        protected virtual T ValidateShape(Shape shape) 
        {
            if (shape != null && shape.Points?.Length != shape.PointsCount)
            {
                return null;
            }
            return shape as T;
        }
        
        public Shape CreateShape()
        {
            return new T();
        }

        public void Draw(Shape shape, Graphics g)
        {
            var targer = ValidateShape(shape);
            if (targer != null)
            {
                if (targer.Brush == null)
                {
                    targer.Brush = new SolidBrush(Color.Black);
                }
                if (targer.Pen == null)
                {
                    targer.Pen = new Pen(Color.Black);
                }

                OnDraw(targer, g);
            }
        }

        protected abstract void OnDraw(T shape, Graphics g);
    }
}