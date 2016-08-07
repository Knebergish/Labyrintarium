using System.Collections.Generic;

using TestOpenGL.PhisicalObjects;


namespace TestOpenGL
{
    interface IEquipmentable
    {
        List<Item> GetAllEquipmentItems();
        Item GetEquipedItem(Section section);
        bool EquipFromBag(int index);
        bool EquipFromWithout(Item item);
        bool Unequip(Section section);

        event TEventDelegate<IEquipmentable> ChangeEquipmentEvent;
    }
}