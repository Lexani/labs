using System.Collections.Generic;
using System.IO;

namespace BLL
{
    public class PluginWatcher
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Run(string pluginsFolder)
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = pluginsFolder,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.dll"
            };
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;

            watcher.EnableRaisingEvents = true;
        }
        
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Notify();
        }

        public void Add(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Remove(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.OnPluginsUpdated();
            }
        }
    }
}