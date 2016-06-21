using System.Collections.Generic;
using System.Windows.Forms;

namespace TestOpenGL
{
    class Control
    {
        public EventDelegate ChangeEnabledControl;
        private bool isEnabledControl;

        public bool IsEnabledControl
        {
            get { return isEnabledControl; }
            set 
            { 
                isEnabledControl = value;

                if (ChangeEnabledControl != null)
                    ChangeEnabledControl();
            }
        }
        public Control()
        {
            IsEnabledControl = true;
        }

        public void ProcessingKeyPress(KeyEventArgs kea)
        {
            if (!IsEnabledControl) 
                return;

            IsEnabledControl = false;

            char key = char.ToLower((char)kea.KeyCode);
            switch (kea.Shift)
            {
                case false:
                    switch (key)
                    {
                        case 'a':
                            if (Program.GCycle.Gamer.isSpawned)
                                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Left);
                            break;
                        case 'w':
                            if (Program.GCycle.Gamer.isSpawned)
                                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Up);
                            break;
                        case 'd':
                            if (Program.GCycle.Gamer.isSpawned)
                                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Right);
                            break;
                        case 's':
                            if (Program.GCycle.Gamer.isSpawned)
                                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Down);
                            break;

                        case 'j':
                            Program.P.Camera.Sight.MoveSight(Direction.Left);
                            break;
                        case 'i':
                            Program.P.Camera.Sight.MoveSight(Direction.Up);
                            break;
                        case 'l':
                            Program.P.Camera.Sight.MoveSight(Direction.Right);
                            break;
                        case 'k':
                            Program.P.Camera.Sight.MoveSight(Direction.Down);
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
            IsEnabledControl = true;
        }

    }
}
