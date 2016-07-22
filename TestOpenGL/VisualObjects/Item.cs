using System;
using System.Collections.Generic;
using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    class Item: IInfoble
    {
        //Texture texture;
        int price;
        
        // Части тела, занимаемые предметом
        List<Part> parts;

        GraphicsObject graphicsObject;

        ObjectInfo objectInfo;
        //-------------


        public Item(Item item)
            :this(item.ObjectInfo, item.GraphicsObject, item.Price, item.Parts) { }
        public Item(ObjectInfo objectInfo, GraphicsObject graphicsObject, int price, List<Part> parts)
        {
            this.graphicsObject = graphicsObject;

            this.price = price;

            this.parts = new List<Part>(parts);
            this.objectInfo = objectInfo;
        }

        public int Price
        { get { return price; } }

        internal List<Part> Parts
        { get { return parts; } }

        public ObjectInfo ObjectInfo
        { get { return objectInfo; } }

        public GraphicsObject GraphicsObject
        { get { return graphicsObject; } }
        //=============
    }
}
