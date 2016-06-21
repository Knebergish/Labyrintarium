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
            formInventory = new Form2(/*new EventDelegate(Program.GCycle.StartStep)*/);
            formMapEditor = new Form3(/*new EventDelegate(Program.GCycle.StartStep)*/);
        }

        public void UpdateForms()
        {
            formInventory.ChangeGamer();
        }

        delegate void openForm();
        public void ProcessingOpeningForms(int num)
        {
            openForm openForm;
            switch (num)
            {
                case 1:
                    openForm = ShowInventory;
                    break;

                case 2:
                    openForm = ShowMapEditor;
                    break;

                default:
                    openForm = (() => { });
                    break;
            }
            Program.mainForm.Invoke(
            new Func<int>(() => 
            {
                openForm();
                return 0;
            }));

        }
        public void ShowInventory()
        {
            //Program.GCycle.StopStep();
            formInventory.Show();
        }
        public void ShowMapEditor()
        {
            //Program.GCycle.StopStep();
            formMapEditor.Show();
        }
    }
}
