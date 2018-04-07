using System.Drawing;
using System.Windows.Forms;
using BLL.Drawing;
using BLL.Shapes;

namespace OOPL.Forms.UserActions
{
    public class DrawShapeController : IUserController
    {
        private int _pointsCount;

        public int PointsCount
        {
            get { return _pointsCount; }
            set
            {
                _pointsCount = value;
                _clicks = 0;
                _points = new Point[value];
            }
        }

        private int _clicks;
        private Shape _shape;
        private Point[] _points;

        public void MouseUp(Point pos)
        {
        }

        public Shape CurrentShape
        {
            get { return _shape; }
            set
            {
                _shape = value;
                PointsCount = _shape.PointsCount;
            }
        }

        public Form1 CurrentForm { get; set; }

        public DrawShapeController(Form1 form)
        {
            CurrentForm = form;
        }
        
        
        public void Click(Point pos)
        {
            if (_shape == null)
            {
                return;
            }

            if (_clicks < PointsCount)
            {
                _points[_clicks] = pos;
                _clicks++;
            }
            if (_clicks == PointsCount)
            {
                _shape.Points = _points;
                _shape.Brush = new SolidBrush(CurrentForm.CurrentColor);
                _shape.Pen = new Pen(CurrentForm.CurrentColor);
                CurrentForm.Shapes.Push(_shape);
                CurrentForm.listBox1.Items.Add(_shape);
                CurrentForm.Redraw();
                CurrentShape = CurrentForm.CurrentDrawer.CreateShape();
                
            }
        }

        public void MouseDown(Point pos)
        {
            
        }
    }
}