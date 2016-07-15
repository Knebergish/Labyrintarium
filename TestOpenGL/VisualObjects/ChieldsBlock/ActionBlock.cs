using System;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsBlock
{
    class ActionBlock : Block, IUsable
    {
        Action action;
        //-------------


        public ActionBlock(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture, Action action) 
            : base(id, name, description, passableness, transparency, permeability, texture)
        {
            this.action = action;
        }

        public Action Action
        { set { action = value; } }
        //=============


        public void Used()
        {
            action();
        }
    }
}
