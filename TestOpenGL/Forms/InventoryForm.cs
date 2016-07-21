using System;
using System.Windows.Forms;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Forms
{
    public partial class InventoryForm : Form
    {
        //EventDelegate closeForm;
        public InventoryForm(/*EventDelegate closeForm*/)
        {
            InitializeComponent();
            //this.closeForm = closeForm;
        }

        public void ChangeGamer()
        {
            if (Program.GCycle.Gamer != null)
            {
                Program.GCycle.Gamer.Inventory.EventsInventory.EventInventoryChangeBag += ReloadListInventory;
                Program.GCycle.Gamer.Inventory.EventsInventory.EventInventoryChangeEquipment += ReloadListInventory;
            }
            ReloadListInventory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Program.GCycle.Gamer.Inventory.UnequipItem(listBox1.SelectedIndex);
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
                if(!Program.GCycle.Gamer.Inventory.EquipItem(listBox2.SelectedIndex))
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

            foreach(Item i in Program.GCycle.Gamer.Inventory.GetBagItems())
            {
                listBox2.Items.Add(i.ObjectInfo.Name);
            }
            foreach (Item i in Program.GCycle.Gamer.Inventory.GetEquipmentItems())
            {
                listBox1.Items.Add(i.ObjectInfo.Name);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Program.GCycle.Gamer.Inventory.EventsInventory.EventInventoryChangeBag += new VoidEventDelegate(ReloadListInventory);
            Program.GCycle.Gamer.Inventory.EventsInventory.EventInventoryChangeEquipment += new VoidEventDelegate(ReloadListInventory);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            //closeForm();
        }
    }
}
