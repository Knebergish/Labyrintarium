using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    class Decal : VisualObject
    {
        public Decal(Decal decal)
            : this(decal.Texture) { }
        public Decal(Texture texture)
            : base(texture) { }
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
