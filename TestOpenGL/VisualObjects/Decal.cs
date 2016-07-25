namespace TestOpenGL.VisualObjects
{
    class Decal : PhisicalObject
    {
        public Decal(Decal decal)
            : this(decal.GraphicObjectsPack) { }
        public Decal(GraphicObjectsPack graphicObjectsPack)
            : base(graphicObjectsPack, new ObjectInfo(0, "", "")) { }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                Program.L.GetMap<Decal>().AddObject(this);
                Program.P.AddRenderObject(GraphicObjectsPack);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return Program.L.GetMap<Decal>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            Program.L.GetMap<Decal>().RemoveObject(PartLayer, Coord);
            Program.P.RemoveRenderObject(GraphicObjectsPack);
        }
    }
}
