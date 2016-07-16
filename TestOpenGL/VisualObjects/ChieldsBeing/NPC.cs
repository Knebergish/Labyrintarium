namespace TestOpenGL.VisualObjects.ChieldsBeing
{
    class NPC : Being, IUsable
    {
        string talkString;
        public VoidEventDelegate useDelegate;
        //-------------


        public NPC(Being being, string talkString, VoidEventDelegate useDelegate)
        : base(being.ObjectInfo.Id, being.ObjectInfo.Name, being.ObjectInfo.Description, being.Texture, being.Alliance)
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
            Features.ActionPoints = 0;
        }

        public void Used()
        {
            System.Windows.Forms.MessageBox.Show(talkString);
            useDelegate?.Invoke();
        }
    }
}
