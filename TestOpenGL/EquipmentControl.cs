using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestOpenGL
{
    partial class EquipmentControl : UserControl
    {
        IEquipmentable equipment;
        //-------------


        public EquipmentControl()
        {
            InitializeComponent();
        }

        public void SetEquipment(IEquipmentable equipment)
        {
            this.equipment = equipment;
            ReloadEquipmentListBox();
        }

        void ReloadEquipmentListBox()
        {
            equipmentListBox.Items.Clear();
            equipment?.GetAllEquipmentItems().ForEach(i => { equipmentListBox.Items.Add(i.ObjectInfo.Name); });
        }
    }
}
