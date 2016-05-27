using System.Threading;
using System.Collections.Generic;


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
        public Sight sight;
        Gamer gamer;

        internal Gamer Gamer
        {
            get { return gamer; }
            set 
            { 
                gamer = value;
                Program.FA.UpdateForms();
                Program.P.Camera.SetLookingBeing(gamer);
            }
        }


        
        public GameCycle()
        {
            
            Steps = new System.Threading.Thread(StepBeings);
            Steps.Start(this.isNextStep);

            sight = new Sight(Program.P.Camera);

            Program.P.StartRedraw();
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
            //Attack A;
            //System.Threading.Thread.Sleep(1000);
            //A = Program.OB.GetAttack(0);
            // Ходы всех сущностей
            while (true)
            {
                    
                for (int currentBeing = 0; currentBeing < Program.L.CountBeings; currentBeing++ )
                {
                    B = Program.L.GetBeing(currentBeing);
                    if (B.isSpawned)
                        B.Step();
                }
                //Program.L.ClearDeadBeings();

                // И тут должны идти уровневые триггеры.
                if (Triggers.currentTriggers != null)
                    Triggers.currentTriggers.CallAllTriggers();

                MRE.WaitOne();
            }
        }
    }
}
