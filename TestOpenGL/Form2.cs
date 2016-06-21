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
    public partial class Form2 : Form
    {
        //EventDelegate closeForm;
        public Form2(/*EventDelegate closeForm*/)
        {
            InitializeComponent();
            //this.closeForm = closeForm;
        }

        public void ChangeGamer()
        {
            if (Program.GCycle.Gamer != null)
            {
                Program.GCycle.Gamer.inventory.eventsInventory.EventInventoryChangeBag += this.ReloadListInventory;
                Program.GCycle.Gamer.inventory.eventsInventory.EventInventoryChangeEquipment += this.ReloadListInventory;
            }
            ReloadListInventory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.GCycle.Gamer.inventory.UnequipItem(listBox1.SelectedIndex);
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Неверно выбрана снимаемая вещь!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Program.GCycle.Gamer.inventory.EquipItem(listBox2.SelectedIndex))
                {
                    MessageBox.Show("Невозможно надеть вещь.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Неверно выбрана надеваемая вещь!");
            }
        }

        private void ReloadListInventory()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            foreach(Item i in Program.GCycle.Gamer.inventory.GetBagItems())
            {
                listBox2.Items.Add(i.visualObjectInfo.Name);
            }
            foreach (Item i in Program.GCycle.Gamer.inventory.GetEquipmentItems())
            {
                listBox1.Items.Add(i.visualObjectInfo.Name);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Program.GCycle.Gamer.inventory.eventsInventory.EventInventoryChangeBag += new EventDelegate(ReloadListInventory);
            Program.GCycle.Gamer.inventory.eventsInventory.EventInventoryChangeEquipment += new EventDelegate(ReloadListInventory);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            //closeForm();
        }
    }
}
