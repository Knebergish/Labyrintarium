using System.Collections.Generic;

using TestOpenGL.PhisicalObjects;


namespace TestOpenGL
{
    interface IBagable
    {
        int Size { get; }
        int CountItemsInBag { get; }

        bool IsFullBag();
        List<Item> GetAllBagItems();
        Item GetItemFromBag(int index);
        bool AddItemInBag(Item item);
        void RemoveItemFromBag(int index);

        event TEventDelegate<IBagable> ChangeBagEvent;
    }
}
