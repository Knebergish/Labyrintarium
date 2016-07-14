using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using TestOpenGL;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    public partial class Form3 : Form
    {
        ImageList IL;
        //EventDelegate closeForm;
        int[] massId;
        public Form3(/*EventDelegate closeForm*/)
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

        private void FillListContent<T>() where T : VisualObject
        {
            listBox1.Items.Clear();
            foreach (T b in Program.L.GetMap<T>().GetCellVO(new Coord(
                Program.P.Camera.Sight.C.X,
                Program.P.Camera.Sight.C.Y
                )))
                listBox1.Items.Add(b.C.Z + ". " + b.visualObjectInfo.Name);
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

                case TypeVisualObject.Item:
                    FillListContent<Item>();
                    break;

                case TypeVisualObject.Decal:
                    FillListContent<Decal>();
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
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y,
                                comboBox1.SelectedIndex
                                )
                            );
                        break;

                    case TypeVisualObject.Block:
                        Program.OB.GetBlock(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y,
                                comboBox1.SelectedIndex
                                )
                            );
                        break;

                    case TypeVisualObject.Being:
                        Program.OB.GetBot(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y,
                                0
                                )
                            );
                        break;

                    case TypeVisualObject.Decal:
                        Program.OB.GetDecal(massId[listView1.SelectedIndices[0]]).Spawn
                            (
                                new Coord
                                (
                                Program.P.Camera.Sight.C.X,
                                Program.P.Camera.Sight.C.Y,
                                comboBox1.SelectedIndex
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
            this.Hide();
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
                    Program.L.GetMap<Background>().RemoveVO
                        (
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y,
                            comboBox1.SelectedIndex
                            )
                        );
                    break;

                case TypeVisualObject.Block:
                    Program.L.GetMap<Block>().RemoveVO
                        (
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y,
                            comboBox1.SelectedIndex
                            )
                        );
                    break;

                case TypeVisualObject.Being:
                    Program.L.GetMap<Being>().RemoveVO
                        (
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y,
                            0
                            )
                        );
                    break;

                case TypeVisualObject.Decal:
                    Program.L.GetMap<Decal>().RemoveVO
                        (
                            new Coord
                            (
                            Program.P.Camera.Sight.C.X,
                            Program.P.Camera.Sight.C.Y,
                            comboBox1.SelectedIndex
                            )
                        );
                    break;
            }

            ReloadListContent();
        }
    }
}
