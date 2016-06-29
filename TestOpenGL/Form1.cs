using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;


using TestOpenGL;
using TestOpenGL.DataIO;
using TestOpenGL.VisualObjects;
using TestOpenGL.Logic;

namespace TestOpenGL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        Being Be;
        private void Form1_Load(object sender, EventArgs e)
        {
            Program.InitApp(this);

            this.MouseWheel += new MouseEventHandler(ResizeMatrix);
            Program.C.ChangeEnabledControl += ChangeColorBorder;
            Program.P.EventFPSUpdate += SetFPS;

            Form1_SizeChanged(sender, e);



            Be = Program.OB.GetGamer(1);
            for (int i = 1; i < 10; i++)
                Be.inventory.PutBagItem(Program.OB.GetItem(i));

            Program.GCycle.Gamer = (Gamer)Be;

            Program.GCycle.StopStep();
            Be.Spawn(new Coord(0, 0));
            
            //Program.OB.GetBot(1, 1).Spawn(new Coord(5, 5));
            Program.OB.GetBot(1, 0).Spawn(new Coord(1, 0));


            
            Program.GCycle.StartStep();
            //Program.P.Camera.SetLookingVO(Be);

            

            button1_Click(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY - 1; y++)
                {
                    //Program.OB.GetDecal(2).Spawn(new Coord(x, y, 1));
                    Program.OB.GetBackground(1).Spawn(new Coord(x, y, 0));

                    if (rnd.Next(0, 40) == 1)
                    {
                        Program.OB.GetBlock(2).Spawn(new Coord(x, y, 0));
                        Program.OB.GetDecal(5).Spawn(new Coord(x, y + 1, 0));
                    }
                    if (rnd.Next(0, 20) == 1)
                    {
                        Program.OB.GetBlock(1).Spawn(new Coord(x, y, 0));
                    }
                }
            }
            //Program.OB.GetBlock(2).Spawn(new Coord(6, 6, 0));
            //Program.OB.GetBlock(1).Spawn(new Coord(6, 6, 1));
            
            /*List<Block> lt = new List<Block>();
            lt = Program.L.GetMap<Block>().GetCellVO(new Coord(6, 6));
            foreach (Block b in lt)
                MessageBox.Show(b.visualObjectInfo.Name);*/
            //Program.P.Camera.SetLookingVO(Program.L.GetMap<Block>().GetVO(new Coord(6, 6)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.FA.ProcessingOpeningForms(1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(delegate()
                {
                    /*Being B = Program.L.MapBeings.GetBeing(Program.GCycle.sight.AimCoord);
                    if (B != null)
                        B.Damage(1);*/
                    //Attack.AttackAnimation(new Coord(0, 0), new Coord(10, 15), Program.OB.GetDecal(2), 100);
                    Program.P.StopRender();
                }).Start();
            
            /*Program.GCycle.StopStep();
            Stages.Stage_1();
            Program.GCycle.StartStep();*/
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            new System.Threading.Thread(delegate()
                {
                    Program.C.ProcessingKeyPress(e);
                }).Start();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            button1.Left = this.Width - button1.Width - 28;
            button2.Left = this.Width - button2.Width - 28;
            button4.Left = this.Width - button4.Width - 28;
            listBox1.Left = this.Width - listBox1.Width - 28;
            AnT.Width = Math.Min(this.Width - (this.Width - button1.Left) - 28, this.Height - 63);
            AnT.Height = AnT.Width;
            pictureBox1.Top = 0;
            pictureBox1.Left = 0;
            pictureBox1.Width = 10;
            pictureBox1.Height = 10;
            Program.P.SettingVisibleAreaSize();
        }
        public void ChangeColorBorder()
        {
            bool b = Program.C.IsEnabledControl;
            Program.mainForm.Invoke(
            new Func<int>(() => 
            {
                if (b) pictureBox1.BackColor = System.Drawing.Color.Green;
                else pictureBox1.BackColor = System.Drawing.Color.Red;
                return 0;
            })
            );
        }

        public void ResizeMatrix(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Program.P.Camera.Height++;
                Program.P.Camera.Width++;
            }
            else
            {
                Program.P.Camera.Height--;
                Program.P.Camera.Width--;
            }
            Program.P.SettingVisibleAreaSize();
            
        }
        public void SetFPS(int value)
        {
            label1.Text = value.ToString();
        }
    }
}
