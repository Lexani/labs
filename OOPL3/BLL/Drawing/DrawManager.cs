using System;
using System.Collections.Generic;
using System.Drawing;
using BLL.Shapes;
using Rectangle = BLL.Shapes.Rectangle;

namespace BLL.Drawing
{
    public class DrawManager
    {
        private readonly Dictionary<Type, IDrawer> _drawers = new Dictionary<Type, IDrawer>();

        public void AddDefaultDrawers()
        {
            _drawers[typeof(Square)] = new DrawSquare();
            _drawers[typeof(Triangle)] = new DrawTriangle();
            _drawers[typeof(Rectangle)] = new DrawRectangle();
        }

        public void RemoveDrawer<T>() where T : Shape, new()
        {
            var type = typeof(T);
            if (_drawers.ContainsKey(type))
            {
                _drawers.Remove(type);
            }
        }

        public void AddDrawer(Type type, IDrawer drawer)
        {
            if (_drawers.ContainsKey(type))
            {
                throw new ArgumentException($"Drawer for type {type.Name} was added.");
            }
            _drawers.Add(type, drawer);
        }

        public void AddDrawer<T>(IDrawer drawer) where T : Shape, new()
        {
            AddDrawer(typeof(T), drawer);
        }

        public IEnumerable<IDrawer> Drawers => _drawers.Values;
        
        public void Draw(Graphics g, List<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                var type = shape.GetType();
                if (_drawers.ContainsKey(type))
                {
                    _drawers[type].Draw(shape, g);
                }
            }
        }
    }
}