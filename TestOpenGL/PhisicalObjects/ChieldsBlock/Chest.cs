using System;

using TestOpenGL.Forms;

namespace TestOpenGL.PhisicalObjects.ChieldsBlock
{
    class Chest : Block, IUsable
    {
        IBagable bag;
        Func<bool> isOpen;
        string closedMessage;
        //-------------


        public Chest(Block blockChest, IBagable bag, Func<bool> isOpen, string closedMessage)
            : base(blockChest)
        {
            this.bag = bag ?? new Bag();
            this.isOpen = isOpen ?? (() => { return true; });
            this.closedMessage = closedMessage ?? "Этот сундук закрыт.";
        }

        public IBagable Bag
        {
            get { return bag; }
            set { bag = value; }
        }
        public Func<bool> IsOpen
        { set { isOpen = value != null ? value : isOpen; } }
        public string ClosedMessage
        { set { closedMessage = value != null ? value : closedMessage; } }
        //=============


        public void Used()
        {
            //TODO: FormAssistant!!!
            if(!isOpen())
            {
                System.Windows.Forms.MessageBox.Show(closedMessage);
                return;
            }

            ExchangeBagsForm ebf = new ExchangeBagsForm(GlobalData.GCycle.Gamer.Inventory, bag);
            ebf.Show();
        }
    }
}
