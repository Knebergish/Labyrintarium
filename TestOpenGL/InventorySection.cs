using TestOpenGL.VisualObjects;


namespace TestOpenGL
{
    class InventorySection
    {
        Item sectionItem;
        bool isBlocked;
        //-------------


        public InventorySection()
        {

        }

        public Item SectionItem
        { get { return sectionItem; } }

        public bool IsBlocked
        {
            get { return isBlocked; }
            set { isBlocked = value; }
        }
        //=============


        public bool Equip(Item item)
        {
            if(sectionItem == null && !isBlocked)
            {
                sectionItem = item;
                return true;
            }
            return false;
        }
        public Item Unequip()
        {
            Item buffer = null;
            if (!isBlocked)
            {
                buffer = sectionItem;
                sectionItem = null;
            }
            return buffer;
        }
    }
}
