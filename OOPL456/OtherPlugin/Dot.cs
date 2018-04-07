using System.Drawing;

namespace OtherPlugin
{
    public class Dot
    {
        public int X { get; set; }

        public int Y { get; set; }

        public void DrawOnCanvas(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Black), X, Y, 2, 2);
        }
    }
}
