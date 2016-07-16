using System;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsBeing
{
    class Bot: Being
    {
        Action<Being> ai;
        //-------------


        public Bot(Being being, Action<Being> AI)
            : base(being)
        {
            ai = AI;
        }

        public Action<Being> AI
        { set { ai = value; } }
        //=============


        protected override void Action()
        {
            if (ai != null)
                ai(this);
            else
                Features.ActionPoints--;
        }
    }
}
