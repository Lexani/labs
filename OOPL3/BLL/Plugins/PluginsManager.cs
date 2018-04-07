using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BLL.Plugins
{
    public class PluginsManager
    {
        public const string PluginsFolder = "Plugins";

        private List<IShapePlugin> _shapePlugins;
        private List<ISerializationPlugin> _serializationPlugins;

        public IEnumerable<IShapePlugin> ShapePlugins => _shapePlugins;
        public IEnumerable<ISerializationPlugin> SerializationPlugins => _serializationPlugins;
        
        public void LoadPlugins()
        {
            var fileNames = Directory.GetFiles(PluginsFolder);


            ICollection<Assembly> assemblies = new List<Assembly>(fileNames.Length);
            foreach (string dllFile in fileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type pluginType = typeof(IPlugin);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(pluginType.FullName) != null)
                            {
                                pluginTypes.Add(type);
                            }
                        }
                    }
                }
            }

            var shapePluginTypes = pluginTypes.Where(x => typeof (IShapePlugin).IsAssignableFrom(x));
            var serializationPluginTypes = pluginTypes.Where(x => typeof(ISerializationPlugin).IsAssignableFrom(x));

            _shapePlugins = shapePluginTypes.Select(x => (IShapePlugin) Activator.CreateInstance(x)).ToList();
            _serializationPlugins = serializationPluginTypes.Select(x => (ISerializationPlugin) Activator.CreateInstance(x)).ToList();
        }

    }
}