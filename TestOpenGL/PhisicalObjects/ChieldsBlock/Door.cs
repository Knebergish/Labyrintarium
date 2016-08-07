using TestOpenGL.Renders;

namespace TestOpenGL.PhisicalObjects.ChieldsBlock
{
    class Door : Block, IUsable
    {
        bool isClosed;
        bool isBlocked;

        GraphicObject openedDoorGraphicObject, closedDoorGraphicObject;
        //-------------


        public Door(Block blockDoor, GraphicObject openedDoorGraphicObject, bool isClosed, bool isBlocked) 
            : base(blockDoor)
        {
            closedDoorGraphicObject = blockDoor.GraphicObjectsPack.GetAllGraphicObjects()[0];
            this.openedDoorGraphicObject = openedDoorGraphicObject;

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
            GraphicObjectsPack.RemoveGraphicObject("main");
            GraphicObjectsPack.AddGraphicObject("main", ChangePartLayer.Yes, isClosed ? closedDoorGraphicObject : openedDoorGraphicObject);
            Passableness = !isClosed;
            Transparency = !isClosed;
        }
    }
}
