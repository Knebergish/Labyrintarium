using System.Collections.Generic;

using TestOpenGL.Renders;

namespace TestOpenGL.PhisicalObjects.ChieldsItem
{
    class Shield : Item, IEquipable
    {
        int level;
        //-------------


        public Shield(Item item, int level)
            : base(item)
        {
            GraphicObject.SetNewPosition(2, new Coord(0, 0));
            this.level = level;
        }

        public int Level
        { get { return level; } }
        //=============    


    }
}
