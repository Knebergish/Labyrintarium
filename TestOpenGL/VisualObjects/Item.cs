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

        GraphicObject graphicObject;

        ObjectInfo objectInfo;
        //-------------


        public Item(Item item)
            :this(item.GraphicObject, item.ObjectInfo, item.Price, item.Parts) { }
        public Item(GraphicObject graphicObject, ObjectInfo objectInfo, int price, List<Part> parts)
        {
            this.graphicObject = graphicObject;

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

        public GraphicObject GraphicObject
        { get { return graphicObject; } }
        //=============
    }
}
