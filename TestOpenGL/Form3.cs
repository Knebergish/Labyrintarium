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

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    public partial class Form3 : Form
    {
        ImageList IL;
        List<Block> LB;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageList IL = new ImageList();
            Image i = Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Blocks\\1.png");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= Program.L.LengthZ; i++)
                comboBox1.Items.Add(i.ToString());
            comboBox1.SelectedIndex = 0;

            IL = new ImageList();
            LB = new List<Block>();
            listView1.LargeImageList = IL;
            DataTable DT = Program.DBIO.ExecuteSQL("SELECT COUNT(id) FROM Blocks");
            int count = int.Parse(DT.Rows[0][0].ToString());
            for (int i = 1; i <= count; i++)
            {
                DT = Program.DBIO.ExecuteSQL("SELECT imageId FROM Blocks WHERE Blocks.id = " + i);
                LB.Add(Program.OB.GetBlock(i));
                IL.Images.Add(Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Blocks\\" + DT.Rows[0][0].ToString() + ".png"));
                listView1.Items.Add(LB[i - 1].visualObjectInfo.name, i - 1);
            }
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.GCycle.ProcessingKeyPress(e.KeyChar);
            ListReload();
        }
        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Text = "Клава выкл.";
        }
        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Text = "Клава вкл.";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.L.SetBlock(LB[listView1.SelectedIndices[0]],
                new Coord(
                    Program.GCycle.sight.AimCoord.X, 
                    Program.GCycle.sight.AimCoord.Y, 
                    int.Parse(comboBox1.Text) - 1));
            ListReload();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = LB[listView1.SelectedIndices[0]].visualObjectInfo.name;
            label4.Text = LB[listView1.SelectedIndices[0]].visualObjectInfo.description;
        }

        private void ListReload()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Program.L.LengthZ; i++)
                try
                {
                    listBox1.Items.Add(Program.L.GetBlock(new Coord(
                        Program.GCycle.sight.AimCoord.X,
                        Program.GCycle.sight.AimCoord.Y,
                        i)).visualObjectInfo.name);
                }
                catch(Exception e)
                {
                    listBox1.Items.Add("Пусто");
                }
        }
    }
}
