using System.Collections.Generic;

using TestOpenGL.Renders;


namespace TestOpenGL.PhisicalObjects
{
    class Item: IInfoble
    {
        int price;

        Section section;
        List<Section> closedSectionsList;

        IStateble states;

        GraphicObject graphicObject;

        ObjectInfo objectInfo;
        //-------------


        public Item(Item item)
            :this(item.GraphicObject, item.ObjectInfo, item.Price, item.Section, item.ClosedSections, item.States) { }
        public Item(GraphicObject graphicObject, ObjectInfo objectInfo, int price, Section section, List<Section> closedSectionsList, IStateble states)
        {
            this.graphicObject = graphicObject;
            this.objectInfo = objectInfo;
            this.price = price;
            this.section = section;
            this.closedSectionsList = new List<Section>(closedSectionsList);
            this.states = states;
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

        public IStateble States
        { get { return states; } }
        //=============
    }
}
