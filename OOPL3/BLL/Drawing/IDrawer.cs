using System.Drawing;
using BLL.Shapes;

namespace BLL.Drawing
{
    public interface IDrawer
    {
        void Draw(Shape shape, Graphics g);

        Shape CreateShape();
    }
}