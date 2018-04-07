using System.Collections.Generic;
using System.Linq;

namespace BLL.Plugins
{
    public class SingleSerializationPluginDecorator : PluginsManager
    {
        private PluginsManager _pluginsManager;

        public override IEnumerable<ISerializationPlugin> SerializationPlugins
        {
            get
            {
                var p = _pluginsManager.SerializationPlugins;
                return p.Take(1);
            }
        }

        public void SetPluginManager(PluginsManager pluginsManager)
        {
            _pluginsManager = pluginsManager;
        }
    }
}