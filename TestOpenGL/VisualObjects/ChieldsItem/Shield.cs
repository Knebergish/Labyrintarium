using System.Collections.Generic;

using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects.ChieldsItem
{
    class Shield : Item
    {
        public Shield(int id, string name, string description, Texture texture, int level, int price)
            : base(id, name, description, texture, level, price, new List<Part> { Part.LHand })
        { }
    }
}
