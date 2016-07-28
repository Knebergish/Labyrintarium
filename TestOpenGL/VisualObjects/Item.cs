using System.Collections.Generic;

using TestOpenGL.Renders;


namespace TestOpenGL.VisualObjects
{
    class Item: IInfoble
    {
        int price;
        
        // Части тела, занимаемые предметом
        //List<Part> parts;

        Section section;
        List<Section> closedSectionsList;

        GraphicObject graphicObject;

        ObjectInfo objectInfo;
        //-------------


        public Item(Item item)
            :this(item.GraphicObject, item.ObjectInfo, item.Price, item.Section, item.ClosedSections) { }
        public Item(GraphicObject graphicObject, ObjectInfo objectInfo, int price, Section section, List<Section> closedSectionsList)
        {
            this.graphicObject = graphicObject;
            this.objectInfo = objectInfo;
            this.price = price;
            this.section = section;
            this.closedSectionsList = new List<Section>(closedSectionsList);
        }

        public int Price
        { get { return price; } }

        public ObjectInfo ObjectInfo
        { get { return objectInfo; } }

        public GraphicObject GraphicObject
        { get { return graphicObject; } }

        public Section Section
        { get { return section; } }

        public List<Section> ClosedSections
        { get { return closedSectionsList; } }
        //=============
    }
}
