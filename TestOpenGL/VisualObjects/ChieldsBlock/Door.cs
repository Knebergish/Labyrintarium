using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsBlock
{
    class Door : Block, IUsable
    {
        bool isClosed;
        bool isBlocked;

        Texture openedDoorTexture, closedDoorTexture;
        //-------------


        public Door(Block blockDoor, Texture openedDoorTexture, bool isClosed, bool isBlocked) 
            : base(blockDoor)
        {
            //closedDoorTexture = blockDoor.Texture;
            this.openedDoorTexture = openedDoorTexture;

            this.isClosed = isClosed;
            this.isBlocked = isBlocked;

            SetClosedState();
        }
        //=============


        public void Used()
        {
            if (isBlocked)
                return;

            isClosed = !isClosed;

            SetClosedState();
        }

        private void SetClosedState()
        {
            //Texture = isClosed ? closedDoorTexture : openedDoorTexture;
            Passableness = !isClosed;
            Transparency = !isClosed;
        }
    }
}
