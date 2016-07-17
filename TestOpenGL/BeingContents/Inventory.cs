﻿using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;
using TestOpenGL.VisualObjects.ChieldsItem;

namespace TestOpenGL.BeingContents
{
    class Inventory
    {
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

        internal EventInventory EventsInventory
        { get { return eventsInventory; } }
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

            foreach (Item i in listItems)
                if (i is T)
                    lt.Add((T)i);
            return lt;
        }
        public List<T> GetEquipmentItemsByType<T>() where T : Item, IEquipable
        {
            return GetItemByType<T>(equipment);
        }
        public int GetLevelEquipmentItemsByType<T>() where T : Item, IEquipable
        {
            int level = 0;
            List<T> lt = GetEquipmentItemsByType<T>();
            foreach (T t in lt)
                level += t.Level;

            return level;
        }

        /*
        public Weapon GetEquipWeapon()
        {
            List<Weapon> lw = GetItemByType<Weapon>(equipment);
            return lw.Count != 0 ? lw[0] : null;
        }
        public Shield GetEquipShield()
        {
            List<Shield> ls = GetItemByType<Shield>(equipment);
            return ls.Count != 0 ? ls[0] : null;
        }
        public List<Armor> GetEquipArmors()
        {
            return GetItemByType<Armor>(equipment);
        }
        */

        /*public int GetEquipWeaponLevel()
        {
            Weapon i = GetEquipWeapon();
            if (i != null)
                return i.Level;
            else
                return 0;
        }
        
        public int GetEquipShieldLevel()
        {
            Shield i = GetEquipShield();
            if (i != null)
                return i.Level;
            else
                return 0;
        }
        
        public int GetEquipArmorsLevel()
        {
            List<Armor> li = GetEquipArmors();
            if (li.Count == 0)
                return 0;
            else
            {
                int count = 0;
                foreach (Armor i in li)
                    count += i.Level;
                return count;
            }
        }
        */



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
            eventsInventory.InventoryChangeBag();
            eventsInventory.InventoryChangeEquipment();
            return true;
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
            EventInventoryChangeBag?.Invoke();
        }
        public void InventoryChangeEquipment()
        {
            EventInventoryChangeEquipment?.Invoke();
        }
    }
}
