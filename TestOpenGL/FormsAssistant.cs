using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestOpenGL
{
    class FormsAssistant
    {
        Form2 formInventory;
        Form3 formMapEditor;
        Form4 formExchangeInventoryes;

        delegate void openForm();

        public FormsAssistant()
        {
            formInventory = new Form2();
            formMapEditor = new Form3();
            //formExchangeInventoryes = new Form4();

        }

        public void UpdateForms()
        {
            formInventory.ChangeGamer();
        }

        public void ShowInventory()
        {
            ProcessingOpeningForms(delegate { formInventory.Show(); });
        }
        public void ShowMapEditor()
        {
            ProcessingOpeningForms(delegate { formMapEditor.Show(); });
        }
        public void ShowExchangeInventoryes(Inventory primoInventory, Inventory secundoInventory)
        {
            formExchangeInventoryes = new Form4(primoInventory, secundoInventory);
            ProcessingOpeningForms(delegate { formExchangeInventoryes.Show(); });
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
    }
}
