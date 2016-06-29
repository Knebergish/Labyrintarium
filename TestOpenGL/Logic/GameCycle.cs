using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;


using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class GameCycle
    {
        Thread Steps;
        ManualResetEvent isNextStep = new ManualResetEvent(false);
        Gamer gamer;


        public GameCycle()
        {
            Steps = new Thread(StepBeings);
            Steps.Start(this.isNextStep);

            StartStep();
        }


        public Gamer Gamer
        {
            get { return gamer; }
            set
            {
                gamer = value;
                Program.FA.UpdateForms();
                Program.P.Camera.SetLookingVO(gamer);
            }
        }


        public void StartStep()
        {
            isNextStep.Set();
        }
        public void StopStep()
        {
            isNextStep.Reset();
        }

        public void StepBeings(object state)
        {
            ManualResetEvent MRE = (ManualResetEvent)state;
            List<Being> LB = new List<Being>(); ;
            bool flag;
            
            // Ходы всех сущностей
            while (true)
            {
                flag = true;
                while(flag)
                {
                    flag = false;
                    //LB = Program.L.GetMap<Being>().GetAllVO();
                    foreach (Being b in Program.L.GetMap<Being>().GetAllVO())//LB)
                    {
                        if (b.features.ActionPoints >= 1)
                        {
                            b.Step();
                            flag = true;
                        }
                    }
                }
                
                //TODO: как-то надо производить в сущностях
                foreach(Being b in LB)
                {
                    b.features.ActionPoints += b.features.IncreaseActionPoints;
                }

                if (Triggers.currentTriggers != null)
                    Triggers.currentTriggers.CallAllTriggers();

                MRE.WaitOne();
            }
        }
    }
}
