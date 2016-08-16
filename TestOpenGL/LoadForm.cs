using System;
using System.Windows.Forms;


namespace TestOpenGL
{
    partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            Program.InitApp(this);
        }

        private void LoadForm_Shown(object sender, EventArgs e)
        {
            Refresh();

            Stages.Stage_1 S1 = new Stages.Stage_1();
            S1.LoadStage();
            Hide();
            Program.mainForm.Show();
        }

        private void LoadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
