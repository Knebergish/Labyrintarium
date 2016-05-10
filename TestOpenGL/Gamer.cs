using System.Threading;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);

        public Gamer(): base()
        {
            this.eventsBeing.EventBeingEndActionPoints += new EventDelegate(EndStep);
        }

        private void EndStep()
        {
            isEndStep.Set();
        }
        public override void Step()
        {
            
            isEndStep.WaitOne();
            isEndStep.Reset();
        }
    }
}
