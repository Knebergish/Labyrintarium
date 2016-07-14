namespace TestOpenGL.VisualObjects
{
    class Decal : VisualObject
    {
        public Decal(int id, string name, string description, Texture texture)
            : base(id, name, description, texture)
        { }
        //=============


        public override bool Spawn(Coord C)
        {
            if (SetNewCoord(C))
            {
                Program.L.GetMap<Decal>().AddVO(this, C);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyCell(Coord C)
        {
            return Program.L.GetMap<Decal>().GetVO(C) == null ? true : false;
        }
    }
}
