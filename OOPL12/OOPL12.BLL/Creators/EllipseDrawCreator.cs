using System.Drawing;
using OOPL12.BLL.DrawManagers;
using OOPL12.BLL.Shapes;

namespace OOPL12.BLL.Creators
{
    public class EllipseDrawCreator : DrawCreator
    {
        public override IDrawManager Create(System.Drawing.Point[] arrayPoint)
        {
            Ellipse ellipse = new Ellipse(arrayPoint[0], arrayPoint[1], Color.Green, Color.Green);
            return new DrawEllipse(ellipse);
        }
    }
}
