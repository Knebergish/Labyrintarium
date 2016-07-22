using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.Forms
{
    public partial class MapEditorForm : Form
    {
        ImageList IL;
        //EventDelegate closeForm;
        int[] massId;
        public MapEditorForm(/*EventDelegate closeForm*/)
        {
            InitializeComponent();
            //this.closeForm = closeForm;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            for (int i = 0; i <= Program.L.LengthZ - 1; i++)
                comboBox1.Items.Add(i.ToString());
            comboBox1.SelectedIndex = 0;

            comboBox2.Items.Clear();
            foreach (string s in Enum.GetNames(typeof(TypeVisualObject)))
                comboBox2.Items.Add(s);
            comboBox2.SelectedIndex = 0;
        }

        private void ReloadImages()
        {
            TypeVisualObject tvo = (TypeVisualObject)comboBox2.SelectedIndex;
            IL = new ImageList();
            listView1.LargeImageList = IL;
            listView1.Items.Clear();
            
            DataTable DT = Program.DBIO.ExecuteSQL("SELECT * FROM " + tvo.ToString() + "s");
            massId = new int[DT.Rows.Count];

            for (int i = 0; i <= DT.Rows.Count - 1; i++)
            {
                IL.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\" + tvo.ToString() + "s\\" + DT.Rows[i][3].ToString() + ".png"));
                listView1.Items.Add(DT.Rows[i][1].ToString(), i);
                massId[i] = int.Parse(DT.Rows[i][0].ToString());
            }
        }

        private void FillListContent<T>() where T : PhisicalObject, IInfoble
        {
            listBox1.Items.Clear();
            foreach (T b in Program.L.GetMap<T>().GetCellObject(new Coord(
                Program.P.Camera.Sight.C.X,
                Program.P.Camera.Sight.C.Y
                )))
                listBox1.Items.Add(b.PartLayer + ". " + b.ObjectInfo.Name);
        }
        private void ReloadListContent()
        {
            switch ((TypeVisualObject)comboBox2.SelectedIndex)
            {
                case TypeVisualObject.Background:
                    FillListContent<Background>();
                    break;

                case TypeVisualObject.Block:
                    FillListContent<Block>();
                    break;

                case TypeVisualObject.Being:
                    FillListContent<Being>();
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedIndices.Count > 0)
                switch ((TypeVisualObject)comboBox2.SelectedIndex)
                {
                    case TypeVisualObject.Background:
                        Program.OB.GetBackground(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y
                                )
                            );
                        break;

                    case TypeVisualObject.Block:
                        Program.OB.GetBlock(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y
                                )
                            );
                        break;

                    case TypeVisualObject.Being:
                        Program.OB.GetBeing(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                0,
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y
                                )
                            );
                        break;

                    case TypeVisualObject.Decal:
                        Program.OB.GetDecal(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                comboBox1.SelectedIndex,
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y
                                )
                            );
                        break;
                }

            ReloadListContent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeVisualObject tvo = (TypeVisualObject)comboBox2.SelectedIndex;
            DataTable DT = Program.DBIO.ExecuteSQL("SELECT * FROM " + (tvo.ToString() + "s WHERE " + ((TypeVisualObject)comboBox2.SelectedIndex).ToString() + "s.id=" + massId[listView1.SelectedIndices[0]].ToString()));
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
            switch ((TypeVisualObject)comboBox2.SelectedIndex)
            {
                case TypeVisualObject.Background:
                    Program.L.GetMap<Background>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y
                            )
                        );
                    break;

                case TypeVisualObject.Block:
                    Program.L.GetMap<Block>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y
                            )
                        );
                    break;

                case TypeVisualObject.Being:
                    Program.L.GetMap<Being>().RemoveObject
                        (
                            0,
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y
                            )
                        );
                    break;

                case TypeVisualObject.Decal:
                    Program.L.GetMap<Decal>().RemoveObject
                        (
                            comboBox1.SelectedIndex,
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y
                            )
                        );
                    break;
            }

            ReloadListContent();
        }
    }
}
