using System.Drawing;
using OOPL12.BLL.DrawManagers;
using Rectangle = OOPL12.BLL.Shapes.Rectangle;

namespace OOPL12.BLL.Creators
{
    public class RectangleDrawCreator : DrawCreator
    {
        public override IDrawManager Create(Point[] arrayPoint)
        {
            Rectangle rectangle = new Rectangle(arrayPoint[0], arrayPoint[1], Color.Red, Color.Red);
            return new DrawRectangle(rectangle);
        }
    }
}
