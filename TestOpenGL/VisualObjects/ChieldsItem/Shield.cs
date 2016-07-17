using System.Collections.Generic;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsItem
{
    class Shield : Item, IEquipable
    {
        int level;
        //-------------


        public Shield(Item item, int level)
            : base(item)
        {
            this.level = level;
        }

        public int Level
        { get { return level; } }
        //=============    


    }
}
