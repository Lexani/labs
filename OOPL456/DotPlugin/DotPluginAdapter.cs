using System;
using System.Collections.Generic;
using System.Drawing;
using BLL.Drawing;
using BLL.Plugins;
using BLL.Shapes;
using OtherPlugin;

namespace DotPlugin
{
    public class DotWrapper : Shape
    {
        public override int PointsCount { get; } = 1;
        public override string Name { get; } = "Dot";

        public DotWrapper()
        {
            Dot = new Dot();
            PointsProtected = new Point[1];
        }
        

        public Dot Dot { get; set; }
    }

    public class DotDrawer : DrawShape<DotWrapper>
    {
        protected override void OnDraw(DotWrapper shape, Graphics g)
        {
            var dot = shape.Dot;
            var pos = shape.Points[0];
            dot.X = pos.X;
            dot.Y = pos.Y;
            shape.Dot.DrawOnCanvas(g);
        }
    }

    public class DotPluginAdapter : IShapePlugin
    {
        public IEnumerable<KeyValuePair<Type, IDrawer>> RegisterShape()
        {
            yield return new KeyValuePair<Type, IDrawer>(typeof(DotWrapper), new DotDrawer());
        }
    }
}