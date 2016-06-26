using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL
{
    class Inventory
    {
        // Неэкипированные вещи, хранящиеся в мешке.
        List<Item> bag;

        // Экипированные вещи.
        List<Item> equipment;

        public EventInventory eventsInventory;

        public Inventory()
        {
            bag = new List<Item>();
            equipment = new List<Item>();
            eventsInventory = new EventInventory();
        }

        public void PutBagItem(Item i)
        {
            bag.Add(i);
            eventsInventory.InventoryChangeBag();
        }
        public void ThrowBagItem(int num)
        {
            try
            {
                bag.RemoveAt(num);
                eventsInventory.InventoryChangeBag();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Item> GetBagItems()
        {
            return bag;
        }
        public List<Item> GetEquipmentItems()
        {
            return equipment;
        }
        private Item GetItemByPart(Part part, List<Item> listItems)
        {
            foreach (Item i in listItems)
                foreach (Part p in i.Parts)
                    if (p == part)
                        return i;
            return null;
        }
        public Item GetEquipWeapon()
        {
            return GetItemByPart(Part.RHand, equipment);
        }
        public Item GetEquipShield()
        {
            Item i = GetItemByPart(Part.LHand, equipment);
            if (i is Shield)
                return i;
            return null;
        }
        public List<Item> GetEquipArmors()
        {
            List<Item> listArmor = new List<Item>();
            for (int i = 0; i < 3; i++)
            {
                listArmor.Add(GetItemByPart((Part)i, equipment));
            }
            listArmor.RemoveAll(x => x == null);
            return listArmor;
        }

        public bool EquipItem(int num)
        {
            try
            {
                Item ei = bag[num];


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
                eventsInventory.InventoryChangeBag();
                eventsInventory.InventoryChangeEquipment();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UnequipItem(int num)
        {
            try
            {
                bag.Add(equipment[num]);
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
            if (EventInventoryChangeBag != null)
                EventInventoryChangeBag();
        }
        public void InventoryChangeEquipment()
        {
            if (EventInventoryChangeEquipment != null)
                EventInventoryChangeEquipment();
        }
    }
}
