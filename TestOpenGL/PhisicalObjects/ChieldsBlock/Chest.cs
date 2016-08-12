using System;

using TestOpenGL.Forms;

namespace TestOpenGL.PhisicalObjects.ChieldsBlock
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
            //TODO: FormAssistant!!!
            Program.mainForm.Invoke(
                new Action(() => 
                {
                    ExchangeBagsForm ebf = new ExchangeBagsForm(Program.GCycle.Gamer.Inventory, bag);
                    ebf.Show();
                })
                );
            
        }
    }
}
