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
            GlControl.InitializeContexts();
        }

        Being Be;
        private void Form1_Load(object sender, EventArgs e)
        {
            Program.InitApp(this);

            this.MouseWheel += new MouseEventHandler(ResizeMatrix);
            Program.C.ChangeEnabledControl += ChangeColorControlEnabledIndicator;
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

            Program.P.ShadersList.Add(new Func<List<RenderObject>>(() => 
            {
                Block b = Program.OB.GetBlock(3);
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Block block in Program.L.GetMap<Block>().GetAllVO())
                    if (block.Id == 2)
                        if(Analytics.CorrectCoordinate(block.C.X, block.C.Y + 1) && Analytics.IsInCamera(new Coord(block.C.X, block.C.Y + 1), Program.P.Camera))
                            lro.Add(new RenderObject(b.texture, new Coord(block.C.X, block.C.Y + 1), (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + Program.L.LengthZ + 0.5));

                return lro;
            }));
            Program.P.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Being b in Program.L.GetMap<Being>().GetAllVO())
                    if (Analytics.IsInCamera(new Coord(b.C.X, b.C.Y), Program.P.Camera))
                        foreach(Item i in b.inventory.GetEquipmentItems())
                            lro.Add(new RenderObject(i.texture, b.C, (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + 0.1));

                return lro;
            }));

            Program.GCycle.StartStep();

            VariantsControls.StandartGamerControl();

            button1_Click(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY; y++)
                {
                    Program.OB.GetBackground(1).Spawn(new Coord(x, y, 0));
                    if (rnd.Next(0, 40) == 1)
                    {
                        Program.OB.GetBlock(2).Spawn(new Coord(x, y, 0));
                    }
                    if (rnd.Next(0, 20) == 1)
                    {
                        Program.OB.GetBlock(1).Spawn(new Coord(x, y, 0));
                    }
                }
            }

            Program.L.GetMap<Block>().AddVO
                (
                    new Door
                    (
                        new Block
                            (
                                14,
                                "Деревянная дверь",
                                "Дверь, сделанная из дерева",
                                false,
                                false,
                                false,
                                Program.TA.GetTexture(TypeVisualObject.Block, "14")
                            ),
                        Program.TA.GetTexture(TypeVisualObject.Block, "14-opened"),
                        true,
                        false
                    ), 
                    new Coord(5, 5, 3)
                );

            Inventory i = new Inventory();
            i.PutBagItem(Program.OB.GetItem(1));
            i.PutBagItem(Program.OB.GetItem(5));
            Program.L.GetMap<Block>().AddVO
                 (
                     new Chest
                     (
                        Program.OB.GetBlock(14),
                        i
                     ),
                     new Coord(3, 3, 1)
                 );

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
            Program.FA.ShowInventory();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(delegate()
                {
                }).Start();
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
            logListBox.Left = this.Width - logListBox.Width - 28;
            GlControl.Width = Math.Min(this.Width - (this.Width - button1.Left) - 28, this.Height - 63);
            GlControl.Height = GlControl.Width;
            controlEnabledIndicator.Top = 0;
            controlEnabledIndicator.Left = 0;
            controlEnabledIndicator.Width = 10;
            controlEnabledIndicator.Height = 10;
            Program.P.SettingVisibleAreaSize();
        }
        public void ChangeColorControlEnabledIndicator(bool value)
        {
            //bool b = Program.C.IsEnabledControl;
            Program.mainForm.Invoke(
            new Func<int>(() => 
            {
                if (value) controlEnabledIndicator.BackColor = System.Drawing.Color.Green;
                else controlEnabledIndicator.BackColor = System.Drawing.Color.Red;
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
            FPSValueLabel.Text = value.ToString();
        }
    }
}
