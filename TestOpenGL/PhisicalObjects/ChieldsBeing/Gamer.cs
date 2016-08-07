using System.Threading;

namespace TestOpenGL.PhisicalObjects.ChieldsBeing
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);
        //-------------


        public Gamer(Being being)
            : base(being)
        {
            StartStepEvent += new VoidEventDelegate(() => { Program.C.IsEnabledControl = true; isEndStep.Reset();  });
            EndStepEvent += new VoidEventDelegate(() => { Program.C.IsEnabledControl = false; });
            Parameters.EndActionPointsEvent += new VoidEventDelegate(() => { isEndStep.Set(); });
        }
        //=============


        protected override void Action()
        {
            isEndStep.WaitOne();
        }
    }
}
