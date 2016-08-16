using System.Threading;

using TestOpenGL.PhisicalObjects.ChieldsBeing;

namespace TestOpenGL.World
{
    class GameCycle
    {
        // Поток пошаговости.
        Thread ThreadSteps;
        ManualResetEvent isNextStep;
        ManualResetEvent isStopStep;
        Gamer gamer;
        public event VoidEventDelegate EventStepBeings;
        public event VoidEventDelegate EventStepTriggers;
        public event VoidEventDelegate EventStepBeingsIncrease;
        //-------------


        public GameCycle()
        {
            isNextStep = new ManualResetEvent(false);
            isStopStep = new ManualResetEvent(true);
            ThreadSteps = new Thread(Steps);
            ThreadSteps.Start();
        }

        public Gamer Gamer
        {
            get { return gamer; }
            set
            {
                gamer = value;
                //GlobalData.FA.UpdateForms();
                GlobalData.WorldData.Camera.SetLookingVO(gamer);
            }
        }

        public ManualResetEvent IsStopStep
        { get { return isStopStep; } }
        //=============


        public void StartStep()
        {
            isStopStep.Reset();
            isNextStep.Set();
        }
        public void StopStep()
        {
            isNextStep.Reset();
            isStopStep.Set();
        }

        public void Steps()
        {
            while(true)
            {
                isNextStep.WaitOne();

                StepBeings();
                StepTriggers();
                StepBeingsIncrease();

                Program.mainForm.ReloadData();
            }
        }
        void StepBeings()
        {
            EventStepBeings?.Invoke();
        }
        void StepTriggers()
        {
            Triggers.currentTriggers.CallAllTriggers();
        }
        void StepBeingsIncrease()
        {
            EventStepBeingsIncrease?.Invoke();
        }
    }
}
