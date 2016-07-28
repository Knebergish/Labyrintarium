using TestOpenGL.BeingContents;

namespace TestOpenGL.VisualObjects.ChieldsBlock
{
    class Chest : Block, IUsable
    {
        IBagable bag;
        //-------------


        public Chest(Block blockChest, IBagable bag)
            : base(blockChest)
        {
            this.bag = bag;
        }

        public IBagable Bag
        {
            get { return bag; }
            set { bag = value; }
        }
        //=============


        public void Used()
        {
            //TODOTODO
            //Program.FA.ShowExchangeInventoryes(Program.GCycle.Gamer.Inventory, inventory);
        }
    }
}
