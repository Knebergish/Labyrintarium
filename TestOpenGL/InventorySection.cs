using TestOpenGL.VisualObjects;


namespace TestOpenGL
{
    class InventorySection
    {
        Section section;
        Item item;
        bool isBlocked;
        //-------------


        public InventorySection(Section section)
        {
            this.section = section;
        }

        public Item Item
        { get { return item; } }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }

        public Section Section
        { get { return section; } }
        //=============


        public void Equip(Item item)
        {
            this.item = item;
        }
        public Item Unequip()
        {
            Item buffer = item;
            item = null;
            return buffer;
        }
    }
}
