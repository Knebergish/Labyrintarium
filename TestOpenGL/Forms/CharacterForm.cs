using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestOpenGL.Forms
{
    partial class CharacterForm : Form
    {
        public CharacterForm()
        {
            InitializeComponent();
        }

        private void CharacterForm_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("Features", "Характеристика");
            dataGridView1.Columns.Add("Values", "Значение");

            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\Textures\\Beings\\1.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            ReloadData();
        }

        public void ReloadData()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < Enum.GetNames(typeof(Feature)).Length; i++)
                dataGridView1.Rows.Add(Enum.GetName(typeof(Feature), (Feature)i), GlobalData.GCycle.Gamer.Parameters[(Feature)i]);

            label3.Text = GlobalData.GCycle.Gamer.Parameters.CurrentLevel.ToString();

            label6.Text = GlobalData.GCycle.Gamer.Parameters.FreeFeaturesPoints.ToString();
            button1.Enabled = GlobalData.GCycle.Gamer.Parameters.FreeFeaturesPoints >= 1 ? true : false;

            label3.Text = GlobalData.GCycle.Gamer.Parameters.CurrentLevel.ToString();
            label9.Text = GlobalData.GCycle.Gamer.Parameters.CurrentHealth.ToString() + "/" + GlobalData.GCycle.Gamer.Parameters[State.MaxHealth].ToString();
            label11.Text = GlobalData.GCycle.Gamer.Parameters[State.IncreaseHealth].ToString();
            label13.Text = GlobalData.GCycle.Gamer.Parameters.CurrentActionPoints.ToString();
            label15.Text = GlobalData.GCycle.Gamer.Parameters[State.IncreaseActionPoints].ToString();
            label17.Text = GlobalData.GCycle.Gamer.Parameters.CurrentExperience.ToString() + "/" + GlobalData.GCycle.Gamer.Parameters.NextLevelExperience.ToString();
            label18.Text = "Эммануил Закусейлович";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalData.GCycle.Gamer.Parameters.AdditionFeature((Feature)dataGridView1.SelectedRows[0].Index);
            ReloadData();
        }

        private void CharacterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
