using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Door : UsedBlock
    {
        bool isClosed;
        bool isBlocked;

        Texture openedDoorTexture, closedDoorTexture;

        public Door(Block blockDoor, Texture openedDoorTexture, bool isClosed, bool isBlocked) 
            : base(blockDoor.Id, blockDoor.visualObjectInfo.Name, blockDoor.visualObjectInfo.Description, blockDoor.Passableness, blockDoor.Transparency, blockDoor.Permeability, blockDoor.texture)
        {
            closedDoorTexture = blockDoor.texture;
            this.openedDoorTexture = openedDoorTexture;

            this.isClosed = isClosed;
            this.isBlocked = isBlocked;

            SetClosedState();
        }

        public override void Use()
        {
            if (isBlocked)
                return;

            isClosed = !isClosed;

            SetClosedState();
        }

        private void SetClosedState()
        {
            texture = isClosed ? closedDoorTexture : openedDoorTexture;
            Passableness = !isClosed;
            Transparency = !isClosed;
        }
    }
}
