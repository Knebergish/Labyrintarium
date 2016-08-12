using System;
using System.Windows.Forms;

using TestOpenGL.PhisicalObjects;


namespace TestOpenGL.Forms
{
    partial class ExchangeBagsForm : Form
    {
        IBagable primoBag, secundoBag;

        public ExchangeBagsForm(IBagable primoBag, IBagable secundoBag)
        {
            if (primoBag == null || secundoBag == null)
                throw new ArgumentNullException();

            this.primoBag = primoBag;
            this.secundoBag = secundoBag;

            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Program.mainForm.Enabled = false;
            UpdateListBoxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
                return;

            Item i = secundoBag.GetAllBagItems()[listBox2.SelectedIndex];
            secundoBag.RemoveItemFromBag(listBox2.SelectedIndex);
            primoBag.AddItemInBag(i);

            UpdateListBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            Item i = primoBag.GetAllBagItems()[listBox1.SelectedIndex];
            primoBag.RemoveItemFromBag(listBox1.SelectedIndex);
            secundoBag.AddItemInBag(i);

            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            WriteInventoryToListBox(primoBag, listBox1);
            WriteInventoryToListBox(secundoBag, listBox2);

            infoControl1.SetInfo(null);
            infoControl2.SetInfo(null);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.mainForm.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            infoControl1.SetInfo(primoBag.GetAllBagItems()[listBox1.SelectedIndex].ObjectInfo);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            infoControl2.SetInfo(secundoBag.GetAllBagItems()[listBox2.SelectedIndex].ObjectInfo);
        }

        private void WriteInventoryToListBox(IBagable inventory, ListBox listBox)
        {
            listBox.Items.Clear();

            if (inventory.CountItemsInBag > 0)
                foreach (Item i in inventory.GetAllBagItems())
                    listBox.Items.Add(i.ObjectInfo.Name);
        }
    }
}
