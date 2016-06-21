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
            this.eventsBeing.EventBeingStartStep += new EventDelegate(() => { Program.C.IsEnabledControl = true;});
            this.eventsBeing.EventBeingEndStep += new EventDelegate(() => { Program.C.IsEnabledControl = false; });
        }

        public override void Action()
        {
        }
    }
}
