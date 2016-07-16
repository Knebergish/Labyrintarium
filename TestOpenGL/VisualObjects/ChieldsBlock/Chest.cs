using TestOpenGL.BeingContents;

namespace TestOpenGL.VisualObjects.ChieldsBlock
{
    class Chest : Block, IUsable
    {
        Inventory inventory;
        //-------------


        public Chest(Block blockChest, Inventory inventory)
            : base(blockChest)
        {
            this.inventory = inventory;
        }

        internal Inventory Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }
        //=============


        public void Used()
        {
            Program.FA.ShowExchangeInventoryes(Program.GCycle.Gamer.Inventory, inventory);
        }
    }
}
