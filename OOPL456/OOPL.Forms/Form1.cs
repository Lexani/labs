using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BLL.Drawing;
using BLL.Plugins;
using BLL.Serialization;
using BLL.Shapes;
using OOPL.Forms.UserActions;

namespace OOPL.Forms
{
    public partial class Form1 : Form, IObserver
    {
        public DrawManager DrawManager { get; private set; } = new DrawManager();
        public Stack<Shape> Shapes { get; private set; } = new Stack<Shape>();
        public IDrawer CurrentDrawer { get; private set; }
        public Color CurrentColor { get; private set; } = Color.Black;
        public Graphics G { get; private set; }
        public PluginsManager PluginsManager { get; private set; } = new PluginsManager();
        public PluginWatcher PluginWatcher { get; private set; } = new PluginWatcher();
        public SingleSerializationPluginDecorator SingleSerializationPluginDecorator { get; private set; } = new SingleSerializationPluginDecorator();


        public ControllersManager ControllersManager { get; private set; } = new ControllersManager();
        public Shape SelectedShape { get; set; }
        public ShapeSerializer Serializer { get; private set; } = new ShapeSerializer();

        private int _shapesButtonsCount = 0;
        private const int _shapesButtonsOffset = 2;
        private bool pluginEnabled = true;

        public void Redraw()
        {
            G.Clear(Color.White);
            DrawManager.Draw(G, Shapes.Reverse().ToList());
        }

        public Form1()
        {
            InitializeComponent();
            SingleSerializationPluginDecorator.SetPluginManager(PluginsManager);

            PluginWatcher.Add(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ControllersManager.Add(new DrawShapeController(this));
            ControllersManager.Add(new MoveShapeController(this));

            PluginsManager.LoadPlugins();
            PluginWatcher.Run(PluginsManager.PluginsFolder);

            G = panel1.CreateGraphics();

            LoadShapes();


           
        }

        private void LoadShapes()
        {
            DrawManager.AddDefaultDrawers();
            foreach (var keyValuePair in PluginsManager.ShapePlugins.SelectMany(shapePlugin => shapePlugin.RegisterShape()))
            {
                DrawManager.AddDrawer(keyValuePair.Key, keyValuePair.Value);
            }

            
            var index = _shapesButtonsOffset;
            foreach (var shapeDrawer in DrawManager.Drawers)
            {
                var defaultShape = shapeDrawer.CreateShape();
                var button = new ToolStripButton
                {
                    Text = defaultShape.Name,
                    DisplayStyle = ToolStripItemDisplayStyle.Text,
                };
                button.Click += (o, args) =>
                {
                    CurrentDrawer = shapeDrawer;
                    var shape = shapeDrawer.CreateShape();
                    ControllersManager.Set<DrawShapeController>();
                    ControllersManager.Current.CurrentShape = shape;
                    foreach (var btn in toolStrip.Items.OfType<ToolStripButton>())
                    {
                        btn.Checked = false;
                    }
                    button.Checked = true;
                };
                toolStrip.Items.Insert(index++, button);
                _shapesButtonsCount++;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            ControllersManager.Current.Click(e.Location);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            foreach (var btn in toolStrip.Items.OfType<ToolStripButton>())
            {
                btn.Checked = false;
            }
            ((ToolStripButton)sender).Checked = true;
            ControllersManager.Set<MoveShapeController>();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ControllersManager.Current.MouseDown(e.Location);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            ControllersManager.Current.MouseUp(e.Location);
        }

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            if (Shapes.Count > 0)
            {
                Shapes.Pop();
                listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
            }
            Redraw();
        }

        private void panelColor_Click(object sender, EventArgs e)
        {
            using (var d = new ColorDialog())
            {
                if (d.ShowDialog() == DialogResult.OK)
                {
                    CurrentColor = d.Color;
                    panelColor.BackColor = CurrentColor;
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedShape = listBox1.SelectedItem as Shape;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Shapes.Count > 0)
            {
                Shapes.Clear();
                listBox1.Items.Clear();
            }
            Redraw();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var d = new SaveFileDialog())
            {
                d.FileName = "text";
                d.Filter = "Text file|*.txt";
                if (d.ShowDialog() == DialogResult.OK)
                {
                    var shapes = listBox1.Items.OfType<Shape>().ToList();

                    using (var f = File.CreateText(d.FileName))
                    {
                        var text = Serializer.Serialize(shapes);
                        var plugin = SingleSerializationPluginDecorator.SerializationPlugins.FirstOrDefault();
                        if (plugin != null && plugin.Enabled)
                        {
                            text = plugin.OnAfterSerialize(text);
                        }
                        
                        f.Write(text);
                    }
                }
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (var d = new OpenFileDialog())
                {
                    d.Filter = "Text file|*.txt";
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        var text = File.ReadAllText(d.FileName);

                        var plugin = SingleSerializationPluginDecorator.SerializationPlugins.FirstOrDefault();
                        if (plugin != null && plugin.Enabled)
                        {
                            text = plugin.OnBeforeDeserialize(text);
                        }
                        string[] lines = text.Split(
                            new[] { Environment.NewLine },  
                            StringSplitOptions.RemoveEmptyEntries
                        );
                        var shapes = Serializer.Deserialize(lines);


                        Shapes.Clear();
                        listBox1.Items.Clear();

                        foreach (var shape in shapes)
                        {
                            Shapes.Push(shape);
                            listBox1.Items.Add(shape);
                        }
                        Redraw();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid data format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDisablePlugin_Click(object sender, EventArgs e)
        {
            pluginEnabled = !pluginEnabled;
            ((ToolStripButton) sender).Text = pluginEnabled ? "Disable crypto" : "Enable crypto";

            foreach (var plugin in PluginsManager.SerializationPlugins)
            {
                plugin.Enabled = pluginEnabled;
            }
        }

        public void OnPluginsUpdated()
        {
            MessageBox.Show("Plugins were added or deleted.\nReload plugins to see the changes.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReloadPlugins_Click(object sender, EventArgs e)
        {
            DrawManager.Clear();
            for (var i = _shapesButtonsOffset; i < _shapesButtonsCount + _shapesButtonsOffset; i++)
            {
                toolStrip.Items.RemoveAt(_shapesButtonsOffset);
            }
            _shapesButtonsCount = 0;
            PluginsManager.LoadPlugins();
            LoadShapes();
        }
    }
}
