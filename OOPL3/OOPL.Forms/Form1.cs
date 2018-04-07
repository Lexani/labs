using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL.Drawing;
using BLL.Plugins;
using BLL.Serialization;
using BLL.Shapes;
using OOPL.Forms.UserActions;

namespace OOPL.Forms
{
    public partial class Form1 : Form
    {
        public readonly DrawManager DrawManager = new DrawManager();
        public readonly Stack<Shape> Shapes = new Stack<Shape>();
        public IDrawer CurrentDrawer;
        public Color CurrentColor = Color.Black;
        public Graphics G;
        public PluginsManager PluginsManager = new PluginsManager();

        public ControllersManager ControllersManager = new ControllersManager();
        public Shape SelectedShape { get; set; }
        public ShapeSerializer Serializer = new ShapeSerializer();
        
        public void Redraw()
        {
            G.Clear(Color.White);
            DrawManager.Draw(G, Shapes.Reverse().ToList());
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ControllersManager.Add(new DrawShapeController(this));
            ControllersManager.Add(new MoveShapeController(this));

            PluginsManager.LoadPlugins();


            G = panel1.CreateGraphics();

            DrawManager.AddDefaultDrawers();
            foreach (var keyValuePair in PluginsManager.ShapePlugins.SelectMany(shapePlugin => shapePlugin.RegisterShape()))
            {
                DrawManager.AddDrawer(keyValuePair.Key, keyValuePair.Value);
            }


            var index = 2;
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
                        f.Write(Serializer.Serialize(shapes));
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
                        Shapes.Clear();
                        listBox1.Items.Clear();

                        var strs = File.ReadAllLines(d.FileName);
                        var shapes = Serializer.Deserialize(strs);

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
    }
}
