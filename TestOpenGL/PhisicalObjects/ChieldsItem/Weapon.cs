using System.Collections.Generic;
using TestOpenGL.Renders;

namespace TestOpenGL.PhisicalObjects.ChieldsItem
{
    class Weapon : Item, IEquipable
    {
        int level;
        int minDistance;
        int maxDistance;
        int damage;
        //-------------


        public Weapon(Item item, int level, int minDistance, int maxDistance, int damage)
            : base(item)
        {
            GraphicObject.SetNewPosition(1, new Coord(0, 0));
            this.level = level;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.damage = damage;
        }
        public Weapon(GraphicObject graphicObject, ObjectInfo objectInfo, int price, Section section, List<Section> closedSectionsList, IStateble states, int level, int minDistance, int maxDistance, int damage)
            : base(graphicObject, objectInfo, price, section, closedSectionsList, states)
        {
            GraphicObject.SetNewPosition(1, new Coord(0, 0));
            this.level = level;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.damage = damage;
        }


        public int Level
        { get { return level; } }

        public int MinDistance
        { get { return minDistance; } }

        public int MaxDistance
        { get { return maxDistance; } }

        public int Damage
        { get { return damage; } }
        //=============
    }
}
