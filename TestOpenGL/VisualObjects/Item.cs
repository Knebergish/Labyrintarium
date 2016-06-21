using System.Collections.Generic;

namespace TestOpenGL.VisualObjects
{
    class Item : VisualObject
    {
        int level;
        int price;
        // Части тела, занимаемые предметом
        List<Part> parts;

        public int Level
        { get { return level; } }
        public int Price
        { get { return price; } }
        internal List<Part> Parts
        { get { return parts; } }

        public Item(int id, string name, string description, Texture texture, int level, int price, List<Part> parts)
            : base (id, name, description, texture)
        {
            this.level = level;
            this.price = price;

            this.parts = new List<Part>(parts);
        }

        public override bool Spawn(Coord C)
        {
            if (SetNewCoord(C))
            {
                Program.L.GetMap<Item>().AddVO(this, C);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyCell(Coord C)
        {
            return Program.L.GetMap<Item>().GetVO(C) == null ? true : false;
        }
    }
}
