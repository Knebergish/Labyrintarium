using System.Threading;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);

        public Gamer(int id, string name, string description, Texture texture)
            : base(id, name, description, texture)
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
