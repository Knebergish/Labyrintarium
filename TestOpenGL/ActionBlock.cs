using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class ActionBlock : Block, IUsable
    {
        Action action;

        public ActionBlock(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture, Action action) 
            : base(id, name, description, passableness, transparency, permeability, texture)
        {
            this.action = action;
        }

        public Action Action
        { set { action = value; } }

        public void Used()
        {
            action();
        }
    }
}
