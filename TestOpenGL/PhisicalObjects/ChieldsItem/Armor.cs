namespace TestOpenGL.PhisicalObjects.ChieldsItem
{
    class Armor : Item
    {
        int level;
        //-------------


        public Armor(Item item, int level)
            : base(item)
        {
            GraphicObject.SetNewPosition(0, new Coord(0, 0));
            this.level = level;
        }

        public int Level
        { get { return level; } }
        //=============
    }
}
