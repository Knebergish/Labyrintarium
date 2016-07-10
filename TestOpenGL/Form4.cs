using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    partial class Form4 : Form
    {
        Inventory primoInventory, secundoInventory;

        public Form4(Inventory primoInventory, Inventory secundoInventory)
        {
            if (primoInventory == null || secundoInventory == null)
                throw new ArgumentNullException();

            this.primoInventory = primoInventory;
            this.secundoInventory = secundoInventory;

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

            Item i = secundoInventory.GetBagItems()[listBox2.SelectedIndex];
            secundoInventory.ThrowBagItem(listBox2.SelectedIndex);
            primoInventory.PutBagItem(i);

            UpdateListBoxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;

            Item i = primoInventory.GetBagItems()[listBox1.SelectedIndex];
            primoInventory.ThrowBagItem(listBox1.SelectedIndex);
            secundoInventory.PutBagItem(i);

            UpdateListBoxes();
        }

        private void UpdateListBoxes()
        {
            WriteInventoryToListBox(primoInventory, listBox1);
            WriteInventoryToListBox(secundoInventory, listBox2);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.mainForm.Enabled = true;
        }

        private void WriteInventoryToListBox(Inventory inventory, ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (Item i in inventory.GetBagItems())
                listBox.Items.Add(i.visualObjectInfo.Name);
        }
    }
}
