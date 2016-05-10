using System;
using System.Collections.Generic;


using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL
{
    class Inventory
    {
        // Неэкипированные вещи, хранящиеся в мешке.
        List<Item> Bag;

        // Экипированные вещи.
        List<Item> Equipment;

        // Атаки от экипированных вещей
        List<Attack> Attacks;

        public int generalArmor;

        public EventInventory eventsInventory;

        public Inventory()
        {
            Bag = new List<Item>();
            Equipment = new List<Item>();
            Attacks = new List<Attack>();
            eventsInventory = new EventInventory();
        }

        public int CountOutBagItems
        {
            get { return Bag.Count; }
        }
        public int CountEquippedItems
        {
            get { return Equipment.Count; }
        }
        public int CountAttacks
        {
            get { return Attacks.Count; }
        }
        public void PutItem(Item i)
        {
            Bag.Add(i);
            eventsInventory.InventoryChangeBag();
        }
        public void ThrowItem(int num)
        {
            try
            {
                Bag.Remove(GetOutBagItem(num));
                eventsInventory.InventoryChangeBag();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Item GetOutBagItem(int num)
        {
            if (num < 0 || num > Bag.Count)
                throw new IndexOutOfRangeException();
            return Bag[num];
        }
        public Item GetEquippedItem(int num)
        {
            if (num < 0 || num > Equipment.Count)
                throw new IndexOutOfRangeException();
            return Equipment[num];
        }

        public bool EquipItem(int num)
        {
            try
            {
                Item ei = GetOutBagItem(num);


                foreach (Item i in Equipment)
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

                Equipment.Add(ei);
                Bag.Remove(ei);
                eventsInventory.InventoryChangeBag();
                eventsInventory.InventoryChangeEquipment();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UnEquipItem(int num)
        {
            try
            {
                Bag.Add(GetEquippedItem(num));
                Equipment.Remove(GetEquippedItem(num));
                eventsInventory.InventoryChangeBag();
                eventsInventory.InventoryChangeEquipment();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CheckAttack()
        {
            Attacks.Clear();
            foreach(Item i in Equipment)
            {
                foreach (Attack a in i.Attacks)
                    Attacks.Add(a);
            }
        }
        public Attack GetAttack(int num)
        {
            if (num < 0 || num > Attacks.Count)
                throw new IndexOutOfRangeException();
            return Attacks[num];
        }

    }

    class EventInventory
    {
        public event EventDelegate EventInventoryChangeEquipment;
        public event EventDelegate EventInventoryChangeBag;

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
