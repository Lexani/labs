using System.Drawing;
using System.Linq;
using BLL.Shapes;

namespace OOPL.Forms.UserActions
{
    public class MoveShapeController : IUserController
    {
        private Shape _currentShape;

        private Point _startPos;

        private bool _isDrag = false;
        
        public void Click(Point pos)
        {
        }

        public Shape CurrentShape
        {
            get { return _currentShape; }
            set { _currentShape = value; }
        }

        public Form1 CurrentForm { get; set; }

        public MoveShapeController(Form1 form)
        {
            CurrentForm = form;
        }

        public void MouseDown(Point pos)
        {
            if (CurrentForm.SelectedShape != null)
            {
                _currentShape = CurrentForm.SelectedShape;
                _startPos = pos;
                _isDrag = true;
            }
        }

        public void MouseUp(Point pos)
        {
            if (_isDrag)
            {
                _isDrag = false;

                int xoffset = pos.X - _startPos.X,
                    yoffset = pos.Y - _startPos.Y;

                for (var i = 0; i < _currentShape.PointsCount; i++)
                {
                    var p = _currentShape.Points[i];
                    _currentShape.Points[i] = new Point(p.X + xoffset, p.Y + yoffset);
                }

                CurrentForm.Redraw();
            }
        }
    }
}