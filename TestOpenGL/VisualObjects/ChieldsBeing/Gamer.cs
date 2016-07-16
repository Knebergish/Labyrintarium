using System.Threading;

namespace TestOpenGL.VisualObjects.ChieldsBeing
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);
        //-------------


        public Gamer(Being being)
            : base(being)
        {
            EventsBeing.EventBeingStartStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = true; isEndStep.Reset();  });
            EventsBeing.EventBeingEndStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = false; });
            EventsBeing.EventBeingEndActionPoints += new VoidEventDelegate(() => { isEndStep.Set(); });
        }
        //=============


        protected override void Action()
        {
            isEndStep.WaitOne();
        }
    }
}
