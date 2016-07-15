using System.Collections.Generic;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsItem
{
    class Armor : Item
    {
        //-------------


        public Armor(int id, string name, string description, Texture texture, int level, int price, List<Part> parts)
            : base(id, name, description, texture, level, price, parts)
        { }
        //=============
    }
}
