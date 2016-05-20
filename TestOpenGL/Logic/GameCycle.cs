﻿using System.Threading;
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
        public Gamer gamer;
        //public Triggers triggers;
        public bool isEnabledControl;

        Form2 formInventory;
        
        public GameCycle()
        {
            isEnabledControl = true;
            Steps = new System.Threading.Thread(StepBeings);
            Steps.Start(this.isNextStep);

            sight = new Sight(Program.P.camera);

            Program.P.StartRedraw();
            StartStep();

            formInventory = new Form2(new EventDelegate(StartStep));


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

        public void ProcessingKeyPress(char key)
        {
            if (!isEnabledControl) return;
            if (isStopStep) return;
            switch (key)
            {
                case 'a':
                    gamer.Move(0);
                    break;
                case 'w':
                    gamer.Move(1);
                    break;
                case 'd':
                    gamer.Move(2);
                    break;
                case 's':
                    gamer.Move(3);
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
        }

        public void ProcessingOpeningForms(int num)
        {
            if (!isEnabledControl) return;
            switch(num)
            {
                case 1:
                    ShowInventory();
                    break;
            }
        }
        public void ShowInventory()
        {
            StopStep();
            formInventory.Show();
        }
    }
}
