using System;
using System.Threading;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL
{
    class Gamer: Being
    {
        ManualResetEvent isEndStep = new ManualResetEvent(false);

        public Gamer(int id, string name, string description, Texture texture, int alliance)
            : base(id, name, description, texture, alliance)
        {
            this.eventsBeing.EventBeingStartStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = true; isEndStep.Reset(); });
            this.eventsBeing.EventBeingEndStep += new VoidEventDelegate(() => { Program.C.IsEnabledControl = false; });
            this.eventsBeing.EventBeingEndActionPoint += new VoidEventDelegate(() => { isEndStep.Set(); });
        }

        public override void Action()
        {
            isEndStep.WaitOne();
        }
    }
}
