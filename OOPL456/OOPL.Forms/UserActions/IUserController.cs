using System.Drawing;
using BLL.Shapes;

namespace OOPL.Forms.UserActions
{
    public interface IUserController
    {
        void Click(Point pos);

        void MouseDown(Point pos);
        void MouseUp(Point pos);

        Shape CurrentShape { get; set; }

        Form1 CurrentForm { get; set; }
    }
}