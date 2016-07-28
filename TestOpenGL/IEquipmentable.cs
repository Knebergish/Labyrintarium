using System.Collections.Generic;

using TestOpenGL.VisualObjects;


namespace TestOpenGL
{
    interface IEquipmentable
    {
        List<Item> GetAllEquipmentItems();
        Item GetEquipedItem(Section section);
        bool Equip(int index);
        bool Unequip(Section section);

        event TEventDelegate<IEquipmentable> ChangeEquipmentEvent;
    }
}