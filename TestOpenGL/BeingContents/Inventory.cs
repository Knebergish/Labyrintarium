using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;


namespace TestOpenGL.BeingContents
{
    class Inventory
    {
        Being owner;

        // Неэкипированные вещи, хранящиеся в мешке.
        List<Item> bag;

        // Экипированные вещи.
        List<Item> equipment;

        EventInventory eventsInventory;
        //-------------


        public Inventory()
        {
            bag = new List<Item>();
            equipment = new List<Item>();
            eventsInventory = new EventInventory();
        }

        public EventInventory EventsInventory
        { get { return eventsInventory; } }

        public Being Owner
        {
            get { return owner;  }
            set { owner = value;  }
        }
        //=============


        public void PutBagItem(Item i)
        {
            bag.Add(i);
            eventsInventory.InventoryChangeBag();
        }
        public void ThrowBagItem(int num)
        {
            bag.RemoveAt(num);
            eventsInventory.InventoryChangeBag();
        }

        public List<Item> GetBagItems()
        {
            return bag;
        }
        public List<Item> GetEquipmentItems()
        {
            return equipment;
        }

        //TODO: возможно сделать static, как советует MS
        private Item GetItemByPart(Part part, List<Item> listItems)
        {
            foreach (Item i in listItems)
                foreach (Part p in i.Parts)
                    if (p == part)
                        return i;
            return null;
        }
        private List<T> GetItemByType<T>(List<Item> listItems) where T : Item
        {
            List<T> lt = new List<T>();
            listItems.ForEach((Item i) => { if (i is T) lt.Add((T)i); });

            return lt.Count == 0 ? null : lt;
        }
        public List<T> GetEquipmentItemsByType<T>() where T : Item, IEquipable
        {
            return GetItemByType<T>(equipment);
        }
        public int GetLevelEquipmentItemsByType<T>() where T : Item, IEquipable
        {
            int level = 0;
            GetEquipmentItemsByType<T>()?.ForEach((T t) => { level += t.Level; });

            return level;
        }

        public bool EquipItem(int num)
        {
            Item ei = bag[num];
            if (!(ei is IEquipable))
                return false;

            foreach (Item i in equipment)
            {
                foreach (Part p in i.Parts)
                {
                    foreach (Part pp in ei.Parts)
                    {
                        if (p == pp)
                        {
                            return false;
                        }
                    }
                }
            }

            equipment.Add(ei);
            bag.Remove(ei);
            owner.GraphicObjectsPack.AddGraphicObject(ei.GetType().Name, ChangePartLayer.No, ei.GraphicObject);
            eventsInventory.InventoryChangeBag();
            eventsInventory.InventoryChangeEquipment();

            return true;
        }
        public void UnequipItem(int num)
        {
            try
            {
                bag.Add(equipment[num]);
                owner.GraphicObjectsPack.RemoveGraphicObject(equipment[num].GetType().Name);
                equipment.RemoveAt(num);
                
                eventsInventory.InventoryChangeBag();
                eventsInventory.InventoryChangeEquipment();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    class EventInventory
    {
        public event VoidEventDelegate EventInventoryChangeEquipment;
        public event VoidEventDelegate EventInventoryChangeBag;

        public void InventoryChangeBag()
        {
            EventInventoryChangeBag?.Invoke();
        }
        public void InventoryChangeEquipment()
        {
            EventInventoryChangeEquipment?.Invoke();
        }
    }
}
