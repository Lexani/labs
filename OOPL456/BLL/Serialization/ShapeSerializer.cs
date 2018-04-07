using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using BLL.Plugins;
using BLL.Shapes;

namespace BLL.Serialization
{
    public class ShapeSerializer
    {
        public string Serialize(List<Shape> shapes)
        {
            var sb = new StringBuilder();
            foreach (var shape in shapes)
            {
                Serialize(shape, sb);
            }

            return sb.ToString();
        }

        private void Serialize(Shape shape, StringBuilder sb)
        {
            var type = shape.GetType();

            var assembly = type.Assembly;
            var name = type.FullName;
            sb.AppendLine($"{assembly};{name};{shape.PointsCount}");
            sb.AppendLine($"{GetColor(shape.Brush)}");
            sb.AppendLine($"{GetColor(shape.Pen)}");
            foreach (var point in shape.Points)
            {
                sb.AppendLine(point.ToString());
            }
        }

        private string GetColor(Pen pen)
        {
            if (pen == null)
            {
                return string.Empty;
            }
            var color = pen.Color;
            return $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        private string GetColor(SolidBrush brush)
        {
            if (brush == null)
            {
                return string.Empty;
            }
            var color = brush.Color;
            return $"{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        public IList<Shape> Deserialize(string[] input)
        {
            var i = 0;
            var result = new List<Shape>();
            while (i < input.Length)
            {
                var split = input[i].Split(';');
                var assemblyName = split[0];
                var typeName = split[1];
                var pointsCountString = split[2];

                Assembly assembly = null;

                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch (Exception ex)
                {
                    var fileNames = Directory.GetFiles(PluginsManager.PluginsFolder);
                    foreach (var fileName in fileNames)
                    {
                        AssemblyName an = AssemblyName.GetAssemblyName(fileName);
                        if (an.FullName == assemblyName)
                        {
                            assembly = Assembly.Load(an);
                            break;
                        }
                        
                    }
                    if (assembly == null)
                    {
                        i++;
                        continue;
                    }
                }
                
                var type = assembly.GetType(typeName);

                Shape shape;

                if (type != null)
                {
                    shape = Activator.CreateInstance(type) as Shape;
                    if (shape == null)
                    {
                        continue;
                    }
                }
                else
                {
                    i++;
                    continue;
                }

                var pointsCount = int.Parse(pointsCountString);
                var brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(input[++i], 16)));
                var pen = new Pen(Color.FromArgb(Convert.ToInt32(input[++i], 16)));
                var points = new Point[pointsCount];
                for (int j = 0; j < pointsCount; j++)
                {
                    var pstr = input[++i];
                    points[j] = ParsePoint(pstr);
                }
                shape.Points = points;
                shape.Brush = brush;
                shape.Pen = pen;
                result.Add(shape);
                i++;
            }
            return result;
        }

        private static readonly Regex PointRegex = new Regex(@"\{X=(\d+),Y=(\d+)", RegexOptions.Compiled);

        protected static Point ParsePoint(string input)
        {
            var match = PointRegex.Match(input);
            var xstring = match.Groups[1].Value;
            var ystring = match.Groups[2].Value;
            int x, y;
            int.TryParse(xstring, out x);
            int.TryParse(ystring, out y);
            return new Point(x, y);
        }

    }
}