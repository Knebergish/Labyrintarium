namespace TestOpenGL.VisualObjects.ChieldsItem
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
