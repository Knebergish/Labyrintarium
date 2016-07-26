using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using TestOpenGL.Forms;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.Forms
{
    partial class MapEditorForm : Form
    {
        ImageList IL;
        //EventDelegate closeForm;
        int[] massId;

        public MapEditorForm(/*EventDelegate closeForm*/)
        {
            InitializeComponent();
            contentControl1.SetSight(Program.P.Camera.Sight);
            //this.closeForm = closeForm;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i <= Program.L.LengthZ - 1; i++)
                comboBox1.Items.Add(i.ToString());
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Clear();
            foreach (string s in Enum.GetNames(typeof(Layer)))
                comboBox2.Items.Add(s);
            comboBox2.SelectedIndex = 0;

            
        }

        private void ReloadImages()
        {
            Layer layer = (Layer)comboBox2.SelectedIndex;
            IL = new ImageList();
            listView1.LargeImageList = IL;
            listView1.Items.Clear();
            
            DataTable DT = Program.DBIO.ExecuteSQL("SELECT * FROM " + layer.ToString() + "s");
            massId = new int[DT.Rows.Count];

            /*for (int i = 0; i <= DT.Rows.Count - 1; i++)
            {
                IL.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\" + tvo.ToString() + "s\\" + DT.Rows[i][3].ToString() + ".png"));
                listView1.Items.Add(DT.Rows[i][1].ToString(), i);
                massId[i] = int.Parse(DT.Rows[i][0].ToString());
            }*/
            PictureBox pb = new PictureBox();
            Controls.Add(pb);
            pb.Width = 100;
            pb.Height = 200;
            pb.Top = 0;
            pb.Left = 0;
            //Image ima = Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Beings\\2.png");
            Image ima1 = Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Beings\\3-2.png");
            Image ima2 = Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Beings\\3-5.png");
            ImageList il = new ImageList();
            //il.TransparentColor = Color.FromArgb(1, 1, 1);
            il.Images.Add(ima1);
            il.Images.Add(ima2);

            Bitmap b = new Bitmap(100, 200);
            
            il.Draw(Graphics.FromImage(pb.Image), 0, 0, 0);
            il.Draw(Graphics.FromImage(pb.Image), 0, 100, 1);
            
        }

        private void FillListContent<T>() where T : PhisicalObject, IInfoble
        {
            listBox1.Items.Clear();
            foreach (T b in Program.L.GetMap<T>().GetCellObject(new Coord(
                Program.P.Camera.Sight.Coord.X,
                Program.P.Camera.Sight.Coord.Y
                )))
                listBox1.Items.Add(b.PartLayer + ". " + b.ObjectInfo.Name);
        }
        private void ReloadListContent()
        {
            switch ((Layer)comboBox2.SelectedIndex)
            {
                case Layer.Background:
                    FillListContent<Background>();
                    break;

                case Layer.Block:
                    FillListContent<Block>();
                    break;

                case Layer.Being:
                    FillListContent<Being>();
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedIndices.Count > 0)
                switch ((Layer)comboBox2.SelectedIndex)
                {
                    case Layer.Background:
                        Program.OB.GetBackground(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.Coord.X,
                                Program.P.Camera.Sight.Coord.Y
                                )
                            );
                        break;

                    case Layer.Block:
                        Program.OB.GetBlock(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.Coord.X,
                                Program.P.Camera.Sight.Coord.Y
                                )
                            );
                        break;

                    case Layer.Being:
                        Program.OB.GetBeing(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                0,
                                new Coord
                                (
                                Program.P.Camera.Sight.Coord.X,
                                Program.P.Camera.Sight.Coord.Y
                                )
                            );
                        break;

                    case Layer.Decal:
                        Program.OB.GetDecal(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.Coord.X,
                                Program.P.Camera.Sight.Coord.Y
                                )
                            );
                        break;
                }

            ReloadListContent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layer layer = (Layer)comboBox2.SelectedIndex;
            DataTable DT = Program.DBIO.ExecuteSQL("SELECT * FROM " + (layer.ToString() + "s WHERE " + ((Layer)comboBox2.SelectedIndex).ToString() + "s.id=" + massId[listView1.SelectedIndices[0]].ToString()));
            label3.Text = DT.Rows[0][1].ToString();
            label4.Text = DT.Rows[0][2].ToString();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            //closeForm();
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            Program.C.ProcessingKeyPress(e);
            ReloadListContent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadImages();
            ReloadListContent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch ((Layer)comboBox2.SelectedIndex)
            {
                case Layer.Background:
                    Program.L.GetMap<Background>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.Coord.X,
                            Program.P.Camera.Sight.Coord.Y
                            )
                        );
                    break;

                case Layer.Block:
                    Program.L.GetMap<Block>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.Coord.X,
                            Program.P.Camera.Sight.Coord.Y
                            )
                        );
                    break;

                case Layer.Being:
                    Program.L.GetMap<Being>().RemoveObject
                        (
                            0,
                            new Coord
                            (
                            Program.P.Camera.Sight.Coord.X,
                            Program.P.Camera.Sight.Coord.Y
                            )
                        );
                    break;

                case Layer.Decal:
                    Program.L.GetMap<Decal>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.Coord.X,
                            Program.P.Camera.Sight.Coord.Y
                            )
                        );
                    break;
            }

            ReloadListContent();
        }
    }
}
