using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

using Tao.Platform.Windows;
using TestOpenGL.PhisicalObjects;


namespace TestOpenGL.Forms
{
    partial class MainForm : Form
    {
        SimpleOpenGlControl glControl;
        public List<KeyEventArgs> keyList = new List<KeyEventArgs>();
        delegate bool TestyaHren();
        event TestyaHren NewKeyPress;
        //-------------


        public MainForm()
        {
            InitializeComponent();
        }
        //=============


        private void Form1_Load(object sender, EventArgs e)
        {
            glControl = ((OpenGLLibrary)GlobalData.LLL)?.GlControl;
            if (glControl == null)
                ExceptionAssistant.NewException(new Exception("Не подключены библиотеки работы с OpenGL."));
            Controls.Add(glControl);


            NewKeyPress += GlobalData.WorldData.Control.mre.Set;

            MouseWheel += new MouseEventHandler(ResizeMatrix);
            GlobalData.WorldData.Control.ChangeEnabledControl += ChangeColorControlEnabledIndicator;
            GlobalData.RenderManager.ChangeActualFPSEvent += SetFPS;
            GlobalData.Sight.ChangeCoordEvent += ReloadGoalInfo;

            Form1_SizeChanged(sender, e);
            

            //TODO: события изменения жизней и очков действия
            //GlobalData.GCycle.Gamer.Parameters. += ReloadGamerInfo;
            //GlobalData.GCycle.Gamer.Parameters. += ReloadGamerInfo;
            ReloadGamerInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalData.FA.ShowInventory();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Thread(delegate()
                {
                    //GlobalData.FA.ShowCharacter();
                    //GlobalData.GCycle.Gamer.Attack(GlobalData.Sight.C);
                    //Battle.Attack(GlobalData.GCycle.Gamer, GlobalData.Sight.C);
                }).Start();
        }

        
        public void ChangeColorControlEnabledIndicator(bool value)
        {
            Invoke(
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
                GlobalData.WorldData.Camera.Height++;
                GlobalData.WorldData.Camera.Width++;
            }
            else
            {
                GlobalData.WorldData.Camera.Height--;
                GlobalData.WorldData.Camera.Width--;
            }
        }

        public void SetFPS(int value)
        {
            Invoke(
            new Action(() =>
            {
                FPSValueLabel.Text = value.ToString();
            })
            );
        }

        public void ReloadData()
        {
            ReloadGamerInfo();
            ReloadGoalInfo();
        }

        public void ReloadGamerInfo()
        {
            if (GlobalData.GCycle.Gamer == null)
                return;

            Program.MainThreadInvoke(
            new Action(() =>
            {
                label3.Text = GlobalData.GCycle.Gamer.Parameters.CurrentHealth + "/" + GlobalData.GCycle.Gamer.Parameters[State.MaxHealth];
                label5.Text = GlobalData.GCycle.Gamer.Parameters.CurrentActionPoints.ToString();
            })
            );
        }
        public void ReloadGoalInfo()
        {
            Program.MainThreadInvoke(
            new Action(() =>
            {
                Being b = GlobalData.WorldData.Level.GetMap<Being>().GetObject(0, GlobalData.Sight.Coord);
                if (b == null)
                    label8.Text = "0/0";
                else
                    label8.Text = b.Parameters.CurrentHealth + "/" + b.Parameters[State.MaxHealth];
            })
            );
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyList.Add(e);
            GlobalData.WorldData.Control.mre.Set();
            //NewKeyPress();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            button2.Left = this.Width - button2.Width - 28;
            button4.Left = this.Width - button4.Width - 28;

            logListBox.Left = this.Width - logListBox.Width - 28;

            glControl.Top = 11;
            glControl.Left = 11;
            glControl.Width = Math.Min(this.Width - (this.Width - button2.Left) - 28, this.Height - 63);
            glControl.Height = glControl.Width;

            controlEnabledIndicator.Top = 0;
            controlEnabledIndicator.Left = 0;
            controlEnabledIndicator.Width = 10;
            controlEnabledIndicator.Height = 10;

            trackBar1.Left = this.Width - trackBar1.Width - 28;

            //GlobalData.RenderManager.SettingVisibleAreaSize();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            GlobalData.RenderManager.SetMaxFPS(trackBar1.Value);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalData.FA.ShowMapEditor();
        }
    }
}
