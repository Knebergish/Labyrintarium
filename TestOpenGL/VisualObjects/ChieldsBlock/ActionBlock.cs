using System;

namespace TestOpenGL.VisualObjects.ChieldsBlock
{
    class ActionBlock : Block, IUsable
    {
        Action action;
        //-------------


        public ActionBlock(Block block, Action action) 
            : base(block)
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
