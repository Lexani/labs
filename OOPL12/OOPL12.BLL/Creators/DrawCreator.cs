using System.Drawing;
using OOPL12.BLL.DrawManagers;

namespace OOPL12.BLL.Creators
{
    public abstract class DrawCreator
    {
        public abstract IDrawManager Create(Point[] arrayPoint);
    }

}
