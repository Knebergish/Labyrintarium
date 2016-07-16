using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestOpenGL.Forms
{
    public partial class CharacterForm : Form
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
                dataGridView1.Rows.Add(Enum.GetName(typeof(Feature), (Feature)i), Program.GCycle.Gamer.features[(Feature)i]);

            label3.Text = Program.GCycle.Gamer.features.CurrentLevel.ToString();

            label6.Text = Program.GCycle.Gamer.features.FreeFeaturesPoints.ToString();
            button1.Enabled = Program.GCycle.Gamer.features.FreeFeaturesPoints >= 1 ? true : false;

            label3.Text = Program.GCycle.Gamer.features.CurrentLevel.ToString();
            label9.Text = Program.GCycle.Gamer.features.CurrentHealth.ToString() + "/" + Program.GCycle.Gamer.features.MaxHealth.ToString();
            label11.Text = Program.GCycle.Gamer.features.IncreaceHealth.ToString();
            label13.Text = Program.GCycle.Gamer.features.ActionPoints.ToString();
            label15.Text = Program.GCycle.Gamer.features.IncreaseActionPoints.ToString();
            label17.Text = Program.GCycle.Gamer.features.CurrentExperience.ToString() + "/" + Program.GCycle.Gamer.features.NextLevelExperience.ToString();
            label18.Text = "Эммануил Закусейлович";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.GCycle.Gamer.features.AdditionFeature((Feature)dataGridView1.SelectedRows[0].Index);
            ReloadData();
        }

        private void CharacterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
