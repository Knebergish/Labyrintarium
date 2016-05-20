using System.Collections.Generic;

namespace TestOpenGL.VisualObjects
{
    class Item : VisualObject
    {
        public Coord C;

        public int armor;

        // Части тела, занимаемые предметом
        public List<Part> Parts;

        //Атаки, добавляемые предметом
        public List<Attack> Attacks;

        public Item(int id, string name, string description, Texture texture)
            : base (id, name, description, texture)
        {
            this.C = new Coord(0, 0);

            Attacks = new List<Attack>();
            Parts = new List<Part>();
        }

    }
}
