using System;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class NPC : Being, IUsable
    {
        string talkString;
        public VoidEventDelegate useDelegate;
        //-------------


        public NPC(Bot bot, string name, string description, string talkString, VoidEventDelegate useDelegate)
        : base(bot.Id, name, description, bot.texture, -1)
        {
            this.talkString = talkString;
            this.useDelegate = useDelegate;
        }

        public string TalkString
        {
            get { return talkString; }
            set { talkString = value; }
        }
        //=============
        

        protected override void Action()
        {
            features.ActionPoints = 0;
        }

        public void Used()
        {
            System.Windows.Forms.MessageBox.Show(talkString);
            useDelegate?.Invoke();
        }
    }
}
