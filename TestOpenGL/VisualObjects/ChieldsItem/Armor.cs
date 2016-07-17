using System.Collections.Generic;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsItem
{
    class Armor : Item, IEquipable
    {
        int level;
        //-------------


        public Armor(Item item, int level)
            : base(item)
        {
            this.level = level;
        }

        public int Level
        { get { return level; } }
        //=============
    }
}
