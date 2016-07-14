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
        Thread ThreadSteps;
        ManualResetEvent isNextStep = new ManualResetEvent(false);
        Gamer gamer;
        public event VoidEventDelegate EventStepBeings;
        public event VoidEventDelegate EventStepTriggers;
        public event VoidEventDelegate EventStepBeingsIncrease;
        //-------------


        public GameCycle()
        {
            ThreadSteps = new Thread(Steps);
            ThreadSteps.Start();
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
        //=============


        public void StartStep()
        {
            isNextStep.Set();
        }
        public void StopStep()
        {
            isNextStep.Reset();
        }

        public void Steps()
        {
            while(true)
            {
                isNextStep.WaitOne();

                StepBeings();
                StepTriggers();
                StepBeingsIncrease();
            }
        }
        void StepBeings()
        {
            EventStepBeings?.Invoke();
        }
        void StepTriggers()
        {
            EventStepTriggers?.Invoke();
        }
        void StepBeingsIncrease()
        {
            EventStepBeingsIncrease?.Invoke();
        }
    }
}
