using System;
using System.Windows.Forms;

using TestOpenGL.VisualObjects;


namespace TestOpenGL.Forms
{
    partial class InventoryForm : Form
    {
        IInventoryble inventory;

        public InventoryForm()
        {
            InitializeComponent();
        }

        public void SetInventory(IInventoryble inventory)
        {
            this.inventory = inventory;

            if(inventory!=null)
            {
                inventory.ChangeEquipmentEvent += ReloadEquipmentList;
                inventory.ChangeBagEvent += ReloadBagList;
            }

            ReloadEquipmentList(inventory);
            ReloadBagList(inventory);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                inventory.Unequip(inventory.GetAllEquipmentItems()[listBox1.SelectedIndex].Section);
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Неверно выбрана снимаемая вещь!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!inventory.Equip(listBox2.SelectedIndex))
            {
                MessageBox.Show("Невозможно надеть вещь.");
            }
        }

        private void ReloadEquipmentList(IEquipmentable equipment)
        {
            listBox1.Items.Clear();
            equipment.GetAllEquipmentItems()?.ForEach(i => { listBox1.Items.Add(i.ObjectInfo.Name); });
        }
        private void ReloadBagList(IBagable bag)
        {
            listBox2.Items.Clear();
            bag.GetAllBagItems()?.ForEach(i => { listBox2.Items.Add(i.ObjectInfo.Name); });
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //ChangeGamer();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
