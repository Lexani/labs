using System.Drawing;
using BLL.Shapes;

namespace OOPL.Forms.UserActions
{
    public class NullUserContrller : IUserController
    {
        public void Click(Point pos)
        {
            
        }

        public void MouseDown(Point pos)
        {
            
        }

        public void MouseUp(Point pos)
        {
            
        }
        
        public Shape CurrentShape { get; set; }
        public Form1 CurrentForm { get; set; }
    }
}