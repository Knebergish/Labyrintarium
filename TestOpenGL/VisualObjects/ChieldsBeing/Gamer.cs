using System.Threading;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsBeing
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);
        //-------------


        public Gamer(int id, string name, string description, Texture texture, int alliance)
            : base(id, name, description, texture, alliance)
        {
            this.eventsBeing.EventBeingStartStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = true; isEndStep.Reset();  });
            this.eventsBeing.EventBeingEndStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = false; });
            this.eventsBeing.EventBeingEndActionPoints += new VoidEventDelegate(() => { isEndStep.Set(); });
        }
        //=============


        protected override void Action()
        {
            isEndStep.WaitOne();
        }
    }
}
