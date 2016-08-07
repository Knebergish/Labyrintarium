using System;
using System.Collections.Generic;
using System.Linq;

using TestOpenGL.PhisicalObjects;


namespace TestOpenGL
{
    class Equipment : IEquipmentable
    {
        List<InventorySection> inventorySectionsList;

        event TEventDelegate<IEquipmentable> changeEquipmentEvent;

        ABDelegate<Item, int> getItemFromBag;
        ABDelegate<bool, Item> addItemInBag;
        ADelegate<int> removeItemFromBag;
        //-------------


        public Equipment(IBagable bag)
        {
            if (bag == null)
                ExceptionAssistant.NewException(new Exception("Передана несуществующая сумка!"));

            inventorySectionsList = new List<InventorySection>();
            //TODO: возможно, стоит сделать автоматическое заполнение всеми элементами Section?..
            inventorySectionsList.Add(new InventorySection(Section.Armor));
            inventorySectionsList.Add(new InventorySection(Section.Shield));
            inventorySectionsList.Add(new InventorySection(Section.Weapon));

            getItemFromBag = bag.GetItemFromBag;
            addItemInBag = bag.AddItemInBag;
            removeItemFromBag = bag.RemoveItemFromBag;
        }

        event TEventDelegate<IEquipmentable> IEquipmentable.ChangeEquipmentEvent
        {
            add { changeEquipmentEvent += value; }
            remove { changeEquipmentEvent -= value; }
        }
        //=============


        InventorySection GetInventorySection(Section section)
        {
            foreach (InventorySection invsec in inventorySectionsList)
                if (invsec.Section == section)
                    return invsec;
            return null;
        }
        bool IsAccessibleSection(Section section)
        {
            if (
                !(GetInventorySection(section)?.IsBlocked ?? true)
                && GetInventorySection(section).Item == null
                )
                return true;
            return false;
        }
        bool Equip(Item item)
        {

            if (IsAccessibleSection(item.Section))
                if (item.ClosedSections.All(IsAccessibleSection))
                {
                    GetInventorySection(item.Section).Equip(item);
                    item.ClosedSections.ForEach(s => { GetInventorySection(s).IsBlocked = true; });
                    return true;
                }
            return false;
        }

        List<Item> IEquipmentable.GetAllEquipmentItems()
        {
            List<Item> li = new List<Item>();
            foreach (InventorySection invsec in inventorySectionsList)
                if (invsec.Item != null)
                    li.Add(invsec.Item);
            return li.Count == 0 ? null : li;
        }
        Item IEquipmentable.GetEquipedItem(Section section)
        {
            return GetInventorySection(section).Item;
        }
        bool IEquipmentable.EquipFromBag(int index)
        {
            Item item = getItemFromBag(index);
            if (Equip(item))
            {
                removeItemFromBag(index);
                changeEquipmentEvent?.Invoke(this);
                return true;
            }
            else
                return false;
        }
        bool IEquipmentable.EquipFromWithout(Item item)
        {
            if (Equip(item))
            {
                changeEquipmentEvent?.Invoke(this);
                return true;
            }
            else
                return false;
        }
        bool IEquipmentable.Unequip(Section section)
        {
            Item item = GetInventorySection(section)?.Unequip();
            item?.ClosedSections.ForEach(s => { GetInventorySection(s).IsBlocked = false; });

            if (addItemInBag(item))
            {
                changeEquipmentEvent?.Invoke(this);
                return true;
            }
            else
            {
                Equip(item);
                return false;
            }
        }
    }
}
