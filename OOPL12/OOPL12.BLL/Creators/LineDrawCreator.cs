using System.Drawing;
using OOPL12.BLL.DrawManagers;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.Creators
{
    public class LineDrawCreator : DrawCreator
    {
        public override IDrawManager Create(Point[] arrayPoint)
        {
            Line line = new Line(arrayPoint[0], arrayPoint[1], Color.Black);
            return new DrawLine(line);
        }
    }
}
