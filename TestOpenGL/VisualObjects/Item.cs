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

        public Item()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();

            //TODO: сделать только параметризованный конструктор.
            this.C = new Coord(0, 0);

            Attacks = new List<Attack>();
            Parts = new List<Part>();
        }
        public Item(Coord C)
        {
            this.C = C;
        }
        
    }
}
