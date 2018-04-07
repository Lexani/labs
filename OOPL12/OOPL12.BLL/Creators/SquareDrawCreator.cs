using System.Drawing;
using OOPL12.BLL.DrawManagers;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.Creators
{
    public class SquareDrawCreator : DrawCreator
    {
        public override IDrawManager Create(System.Drawing.Point[] arrayPoint)
        {
            Square square = new Square(arrayPoint[0], arrayPoint[1], Color.Black, Color.Black);
            return new DrawSquare(square);
        }
    }
}
