using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;


namespace TestOpenGL
{
    class Bag : IBagable
    {
        int size;
        List<Item> bagList;

        event TEventDelegate<IBagable> changeBagEvent;
        //-------------


        public Bag(int size)
        {
            this.size = size;
            bagList = new List<Item>();
        }

        int IBagable.Size
        { get { return size; } }

        int IBagable.CountItemsInBag
        { get { return bagList.Count; } }

        event TEventDelegate<IBagable> IBagable.ChangeBagEvent
        {
            add { changeBagEvent += value; }
            remove { changeBagEvent -= value; }
        }
        //=============


        bool IsFull()
        {
            if (bagList.Count >= size)
                return true;
            return false;
        }
        bool IsCorrectIndex(int index)
        {
            return index < 0 || index >= bagList.Count ? false : true;
        }

        bool IBagable.IsFullBag()
        {
            return IsFull();
        }
        List<Item> IBagable.GetAllBagItems()
        {
            return bagList.Count == 0 ? null : new List<Item>(bagList);
        }
        Item IBagable.GetItemFromBag(int index)
        {
            return IsCorrectIndex(index) ? bagList[index] : null;
        }
        bool IBagable.AddItemInBag(Item item)
        {
            if (IsFull())
                return false;

            bagList.Add(item);
            changeBagEvent?.Invoke(this);
            return true;
        }
        void IBagable.RemoveItemFromBag(int index)
        {
            if (IsCorrectIndex(index))
            {
                bagList.RemoveAt(index);
                changeBagEvent?.Invoke(this);
            }
        }
    }
}
