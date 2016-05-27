using System.Collections.Generic;
using System.Windows.Forms;

namespace TestOpenGL
{
    class Control
    {
        public bool isEnabledControl;
        public Control()
        {
            isEnabledControl = true;
        }

        public void ProcessingKeyPress(KeyEventArgs kea)
        {
            if (!isEnabledControl) return;

            char key = char.ToLower((char)kea.KeyCode);
            switch (kea.Shift)
            {
                case false:
                    switch (key)
                    {
                        case 'a':
                            Program.GCycle.Gamer.Move(0);
                            break;
                        case 'w':
                            Program.GCycle.Gamer.Move(1);
                            break;
                        case 'd':
                            Program.GCycle.Gamer.Move(2);
                            break;
                        case 's':
                            Program.GCycle.Gamer.Move(3);
                            break;

                        case 'j':
                            Program.GCycle.sight.MoveSight(Direction.Left);
                            break;
                        case 'i':
                            Program.GCycle.sight.MoveSight(Direction.Up);
                            break;
                        case 'l':
                            Program.GCycle.sight.MoveSight(Direction.Right);
                            break;
                        case 'k':
                            Program.GCycle.sight.MoveSight(Direction.Down);
                            break;
                    }
                    break;

                case true:
                    switch (key)
                    {
                        case 'i':
                            Program.FA.ProcessingOpeningForms(1);
                            break;
                        case 'm':
                            Program.FA.ProcessingOpeningForms(2);
                            break;
                    }
                    break;
            }
        }
    }
}
