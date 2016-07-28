using System;
using TestOpenGL.BeingContents;

namespace TestOpenGL.Forms
{
    class FormsAssistant
    {
        InventoryForm formInventory;
        MapEditorForm formMapEditor;
        ExchangeBagsForm formExchangeBags;
        CharacterForm formCharacter;

        delegate void openForm();
        //-------------


        public FormsAssistant()
        {
            formInventory = new InventoryForm();
            formMapEditor = new MapEditorForm();
            formCharacter = new CharacterForm();
            //formExchangeInventoryes = new Form4();

        }
        //=============

        //TODO: дописать этот метод
        public void UpdateForms()
        {
            formInventory.SetInventory(Program.GCycle?.Gamer.Inventory);
        }

        private void ProcessingOpeningForms(openForm delegateOpenForm)
        {
            Program.mainForm.Invoke(
            new Func<int>(() => 
            {
                delegateOpenForm();
                return 0;
            }));
        }

        public void ShowInventory()
        {
            ProcessingOpeningForms(delegate { formInventory.Show(); });
        }
        public void ShowMapEditor()
        {
            ProcessingOpeningForms(delegate { formMapEditor.Show(); });
        }
        /*public void ShowExchangeInventoryes(Inventory primoInventory, Inventory secundoInventory)
        {
            formExchangeInventoryes = new ExchangeInventoryesForm(primoInventory, secundoInventory);
            ProcessingOpeningForms(delegate { formExchangeInventoryes.Show(); });
        }*/
        public void ShowCharacter()
        {
            ProcessingOpeningForms(delegate { formCharacter.Show(); });
        }
    }
}
