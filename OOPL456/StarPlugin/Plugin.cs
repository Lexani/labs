using System;
using System.Collections.Generic;
using BLL.Drawing;
using BLL.Plugins;

namespace StarPlugin
{
    public class Plugin : IShapePlugin
    {
        public IEnumerable<KeyValuePair<Type, IDrawer>> RegisterShape()
        {
            yield return new KeyValuePair<Type, IDrawer>(typeof(Star), new DrawStar());
        }
    }
}