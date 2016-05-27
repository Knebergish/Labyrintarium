using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestOpenGL
{
    class FormsAssistant
    {
        Form2 formInventory;
        Form3 formMapEditor;

        public FormsAssistant()
        {
            formInventory = new Form2(new EventDelegate(Program.GCycle.StartStep));
            formMapEditor = new Form3(new EventDelegate(Program.GCycle.StartStep));
        }

        public void UpdateForms()
        {
            formInventory.ChangeGamer();
        }

        public void ProcessingOpeningForms(int num)
        {
            switch (num)
            {
                case 1:
                    ShowInventory();
                    break;

                case 2:
                    ShowMapEditor();
                    break;
            }
        }
        public void ShowInventory()
        {
            Program.GCycle.StopStep();
            formInventory.Show();
        }
        public void ShowMapEditor()
        {
            Program.GCycle.StopStep();
            formMapEditor.Show();
        }
    }
}
