using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Chest : UsedBlock
    {
        Inventory inventory;

        public Chest(Block blockChest, Inventory inventory)
            : base(blockChest.Id, blockChest.visualObjectInfo.Name, blockChest.visualObjectInfo.Description, blockChest.Passableness, blockChest.Transparency, blockChest.Permeability, blockChest.texture)
        {
            this.inventory = inventory;
        }

        internal Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public override void Use()
        {
            Program.FA.ShowExchangeInventoryes(Program.GCycle.Gamer.inventory, inventory);
        }
    }
}
