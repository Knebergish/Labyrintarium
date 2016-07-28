using System;
using System.Windows.Forms;
using System.Threading;
using TestOpenGL.VisualObjects;
using TestOpenGL.BeingContents;
using TestOpenGL.VisualObjects.ChieldsBlock;
using System.Collections.Generic;

namespace TestOpenGL.Forms
{
    partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            GlControl.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.InitApp(this);

            this.MouseWheel += new MouseEventHandler(ResizeMatrix);
            Program.C.ChangeEnabledControl += ChangeColorControlEnabledIndicator;
            Program.P.EventFPSUpdate += SetFPS;
            Program.P.Camera.Sight.EventSight.EventSightChangeCoord += ReloadGoalInfo;

            Form1_SizeChanged(sender, e);

            //button1_Click(sender, e);
            Stages.Stage_1 S1 = new Stages.Stage_1();
            S1.LoadStage();

            Program.GCycle.Gamer.EventsBeing.EventBeingChangeHealth += ReloadGamerInfo;
            Program.GCycle.Gamer.EventsBeing.EventBeingChangeActionPoints += ReloadGamerInfo;
            ReloadGamerInfo();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY; y++)
                {
                    Program.OB.GetBackground(1).Spawn(0, new Coord(x, y));
                    if (rnd.Next(0, 40) == 1)
                    {
                        Program.OB.GetBlock(2).Spawn(0, new Coord(x, y));
                    }
                    if (rnd.Next(0, 20) == 1)
                    {
                        Program.OB.GetBlock(1).Spawn(0, new Coord(x, y));
                    }
                }
            }

            /*Program.L.GetMap<Block>().AddObject
                (
                    new Door
                    (
                        new Block
                            (
                            new ObjectInfo(
                                14,
                                "Деревянная дверь",
                                "Дверь, сделанная из дерева"),
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
                );*/




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

        private void button4_Click(object sender, EventArgs e)
        {
            new Thread(delegate()
                {
                    Program.FA.ShowCharacter();
                    //Program.GCycle.Gamer.Attack(Program.P.Camera.Sight.C);
                    //Battle.Attack(Program.GCycle.Gamer, Program.P.Camera.Sight.C);
                }).Start();
        }

        
        public void ChangeColorControlEnabledIndicator(bool value)
        {
            Program.mainForm.Invoke(
            new Action(() => 
            {
                if (value) controlEnabledIndicator.BackColor = System.Drawing.Color.Green;
                else controlEnabledIndicator.BackColor = System.Drawing.Color.Red;
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

        public void ReloadData()
        {
            ReloadGamerInfo();
            ReloadGoalInfo();
        }

        public void ReloadGamerInfo()
        {
            if (Program.GCycle.Gamer == null)
                return;

            Program.mainForm.Invoke(
            new Action(() =>
            {
                label3.Text = Program.GCycle.Gamer.Features.CurrentHealth + "/" + Program.GCycle.Gamer.Features.MaxHealth;
                label5.Text = Program.GCycle.Gamer.Features.ActionPoints.ToString();
            })
            );
        }
        public void ReloadGoalInfo()
        {
            Program.mainForm.Invoke(
            new Action(() =>
            {
                Being b = Program.L.GetMap<Being>().GetObject(0, Program.P.Camera.Sight.Coord);
                if (b == null)
                    label8.Text = "0/0";
                else
                    label8.Text = b.Features.CurrentHealth + "/" + b.Features.MaxHealth;
            })
            );
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            new Thread(delegate ()
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

            trackBar1.Left = this.Width - trackBar1.Width - 28;

            Program.P.SettingVisibleAreaSize();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Program.P.MaxFPS = trackBar1.Value;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
