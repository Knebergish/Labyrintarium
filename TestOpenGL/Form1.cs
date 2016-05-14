using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;


using TestOpenGL;
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

        Block B, Bg;
        Being Be, Be1;
        Attack A;
        private void Form1_Load(object sender, EventArgs e)
        {
            Program.InitApp(this);

            

            Bg = Program.OB.GetBlock(1);
            B = Program.OB.GetBlock(2);

            
            Be = Program.OB.GetGamer(1);
            Be1 = Program.OB.GetBot(1);
            Program.GCycle.gamer = (Gamer)Be;

            A = Program.OB.GetAttack(0);
            //A.coefficient = 10;
            //A.profilingFeature = Feature.Stamina;
            //Item i = new Item();
            //i.visualObjectInfo.name = "Test item.";
            //i.Attacks.Add(A);
            //Be.inventory.PutItem(i);

            Be.Spawn(new Coord(0, 0));
            Be1.Spawn(new Coord(1, 5));

            button1_Click(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            VisualObjectStructure<Block> BS = new VisualObjectStructure<Block>();

            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY; y++)
                {
                    BS.Push(Bg, new Coord(x, y, 0));

                    if (rnd.Next(0, 10) == 1)
                        BS.Push(B, new Coord(x, y, 1));
                    else
                        BS.Push(null, new Coord(x, y, 1));
                }
            }
            Program.L.SetBlocks(BS);
            Program.P.camera.SetLookingBeing(Be);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.GCycle.ProcessingOpeningForms(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Be.Move(2);
            
            //A.UseAttack(Be, new Coord(1, 5));
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Program.GCycle.ProcessingKeyPress(e.KeyChar);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.Text = "Клава выкл.";
        }

        private void button3_Enter(object sender, EventArgs e)
        {
            button3.Text = "Клава вкл.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.GCycle.isEnabledControl = false;
            Thread Tr = new System.Threading.Thread(delegate() 
                { 
                    A.UseAttack(Program.GCycle.gamer, Program.GCycle.sight.AimCoord); 
                    Program.GCycle.isEnabledControl = true; 
                });
            Tr.Start();
            
            //Form3 F3 = new Form3();
            //F3.Show();
        }
        private void Test(Delegate del)
        {
            A.UseAttack(Program.GCycle.gamer, Program.GCycle.sight.AimCoord);
            Program.GCycle.isEnabledControl = true;
        }
    }
}
