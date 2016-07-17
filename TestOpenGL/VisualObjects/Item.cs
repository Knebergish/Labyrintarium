using System;
using System.Collections.Generic;
using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    class Item: IInfoble
    {
        Texture texture;
        int price;
        
        // Части тела, занимаемые предметом
        List<Part> parts;

        ObjectInfo objectInfo;
        //-------------


        public Item(Item item)
            :this(item.ObjectInfo.Id, item.ObjectInfo.Name, item.ObjectInfo.Description, item.Texture, item.Price, item.Parts) { }
        public Item(int id, string name, string description, Texture texture, int price, List<Part> parts)
        {
            this.texture = texture;

            this.price = price;

            this.parts = new List<Part>(parts);
            objectInfo = new ObjectInfo(id, name, description);
        }

        public int Price
        { get { return price; } }

        public Texture Texture
        { get { return texture; } }

        internal List<Part> Parts
        { get { return parts; } }

        public ObjectInfo ObjectInfo
        { get { return objectInfo; } }
        //=============
    }
}
