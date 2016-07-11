using System.Collections.Generic;
using System.Linq;

using TestOpenGL.VisualObjects;
using TestOpenGL.Logic;
using TestOpenGL;
using System;

namespace TestOpenGL
{
    class Bot: Being
    {
        Action<Being> ai;

        public Bot(int id, string name, string description, Texture texture, int alliance, Action<Being> AI)
            : base(id, name, description, texture, alliance)
        {
            ai = AI;
        }

        internal Action<Being> AI
        { set { ai = value; } }

        protected override void Action()
        {
            if (ai != null)
                ai(this);
            else
                features.ActionPoints--;
        }
    }
}
