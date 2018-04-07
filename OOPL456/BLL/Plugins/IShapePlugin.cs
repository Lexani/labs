using System;
using System.Collections.Generic;
using BLL.Drawing;

namespace BLL.Plugins
{
    public interface IShapePlugin : IPlugin
    {
        IEnumerable<KeyValuePair<Type, IDrawer>> RegisterShape();
    }
}