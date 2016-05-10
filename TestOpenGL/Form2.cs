using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestOpenGL
{
    public partial class Form2 : Form
    {
        EventDelegate closeForm;
        public Form2(EventDelegate closeForm)
        {
            InitializeComponent();
            this.closeForm = closeForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.GCycle.gamer.inventory.UnEquipItem(listBox1.SelectedIndex);
            }
            catch(IndexOutOfRangeException)
            {
                MessageBox.Show("Неверно выбрана снимаемая вещь!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(!Program.GCycle.gamer.inventory.EquipItem(listBox2.SelectedIndex))
                {
                    MessageBox.Show("Невозможно надеть вещь.");
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Неверно выбрана одеваемая вещь!");
            }
        }

        private void ReloadListInventory()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            for(int i = 0; i < Program.GCycle.gamer.inventory.CountEquippedItems;i++)
            {
                listBox1.Items.Add(Program.GCycle.gamer.inventory.GetEquippedItem(i).visualObjectInfo.name);
            }
            for (int i = 0; i < Program.GCycle.gamer.inventory.CountOutBagItems; i++)
            {
                listBox2.Items.Add(Program.GCycle.gamer.inventory.GetOutBagItem(i).visualObjectInfo.name);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Program.GCycle.gamer.inventory.eventsInventory.EventInventoryChangeBag += new EventDelegate(ReloadListInventory);
            Program.GCycle.gamer.inventory.eventsInventory.EventInventoryChangeEquipment += new EventDelegate(ReloadListInventory);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            closeForm();
        }


    }
}
