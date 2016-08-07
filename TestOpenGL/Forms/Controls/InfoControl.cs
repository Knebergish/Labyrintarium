using System.Windows.Forms;

namespace TestOpenGL.Forms.Controls
{
    partial class InfoControl : UserControl
    {
        public InfoControl()
        {
            InitializeComponent();
        }

        public void SetInfo(PhisicalObjects.ObjectInfo objectInfo)
        {
            infoLabel.Text = "";
            infoLabel.Text += objectInfo?.Name + "\n" + objectInfo?.Description;
        }

        private void InfoControl_Resize(object sender, System.EventArgs e)
        {
            infoLabel.Left = 0;
            infoLabel.Top = 0;
            infoLabel.Width = Width;
            infoLabel.Height = Height;
        }
    }
}
