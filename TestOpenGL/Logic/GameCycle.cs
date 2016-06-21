using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;


using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL.Logic
{
    class GameCycle
    {
        // Поток пошаговости.
        System.Threading.Thread Steps;
        bool isStopStep = true;
        ManualResetEvent isNextStep = new ManualResetEvent(false);
        //public Sight sight;
        Gamer gamer;

        internal Gamer Gamer
        {
            get { return gamer; }
            set 
            { 
                gamer = value;
                Program.FA.UpdateForms();
                Program.P.Camera.SetLookingVO(gamer);
            }
        }


        
        public GameCycle()
        {
            
            Steps = new System.Threading.Thread(StepBeings);
            Steps.Start(this.isNextStep);

            //sight = new Sight(Program.P.Camera);

            //Program.P.StartRedraw();
            StartStep();

            /*VisualObjectStructure<Decal> VOSD = new VisualObjectStructure<Decal>();
            VOSD.Push(Program.OB.GetDecal(3), new Coord(5, 5));
            VOSD.Push(Program.OB.GetDecal(3), new Coord(5, 6));
            Program.L.AddDecals(VOSD);
            Decal d;


            d = Program.OB.GetDecal(4);
            d.C = new Coord(10, 7);
            Program.L.AddDecal(d);

            Program.L.RemoveGroupDecals(0);*/
            
        }

        public void StartStep()
        {
            this.isNextStep.Set();
            this.isStopStep = false;
        }
        public void StopStep()
        {
            this.isNextStep.Reset();
            this.isStopStep = true;
        }

        public void StepBeings(object state)
        {
            ManualResetEvent MRE = (ManualResetEvent)state;
            Being B;
            List<Being> LB = new List<Being>(); ;
            bool flag;
            
            // Ходы всех сущностей
            while (true)
            {
                flag = true;
                while(flag)
                {
                    flag = false;
                    LB = Program.L.GetMap<Being>().GetAllVO();
                    foreach (Being b in LB)
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
