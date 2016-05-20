using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    class Stage
    {
        public string name;
        public string description;
        public int lengthX, lengthY, lengthZ;
        public Triggers triggers;
        public delegate void StageDelegate();
        public StageDelegate stageLoad;

        public void StartStage()
        {
            //Program.GCycle.triggers = triggers;
            Program.L = new Logic.Level(lengthX, lengthY, lengthZ);
            stageLoad();
        }
    }
}
