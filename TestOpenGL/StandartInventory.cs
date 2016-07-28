using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;


namespace TestOpenGL
{
    class StandartInventory : IInventoryble
    {
        IEquipmentable equipment;
        IBagable bag;
        //-------------


        public StandartInventory(IEquipmentable equipment, IBagable bag)
        {
            if (equipment == null)
                ExceptionAssistant.NewException(new Exception("Передана несуществующая экипировка!"));
            if (bag == null)
                ExceptionAssistant.NewException(new Exception("Передана несуществующая сумка!"));

            this.equipment = equipment;
            this.bag = bag;
        }

        int IBagable.Size
        { get { return bag.Size; } }
        int IBagable.CountItemsInBag
        { get { return bag.CountItemsInBag; } }

        event TEventDelegate<IBagable> IBagable.ChangeBagEvent
        {
            add { bag.ChangeBagEvent += value; }
            remove { bag.ChangeBagEvent -= value; }
        }
        event TEventDelegate<IEquipmentable> IEquipmentable.ChangeEquipmentEvent
        {
            add { equipment.ChangeEquipmentEvent += value; }
            remove { equipment.ChangeEquipmentEvent -= value; }
        }
        //=============


        List<Item> IEquipmentable.GetAllEquipmentItems()
        { return equipment.GetAllEquipmentItems(); }

        Item IEquipmentable.GetEquipedItem(Section section)
        { return equipment.GetEquipedItem(section); }

        bool IEquipmentable.Equip(int index)
        { return equipment.Equip(index); }

        bool IEquipmentable.Unequip(Section section)
        { return equipment.Unequip(section); }


        bool IBagable.IsFullBag()
        { return bag.IsFullBag(); }

        List<Item> IBagable.GetAllBagItems()
        { return bag.GetAllBagItems(); }

        Item IBagable.GetItemFromBag(int index)
        { return bag.GetItemFromBag(index); }

        bool IBagable.AddItemInBag(Item item)
        { return bag.AddItemInBag(item); }

        void IBagable.RemoveItemFromBag(int index)
        { bag.RemoveItemFromBag(index); }
    }
}
