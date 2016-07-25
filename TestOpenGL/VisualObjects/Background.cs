namespace TestOpenGL.VisualObjects
{
    class Background : Block
    {
        public Background(GraphicObjectsPack graphicObjectsPack, ObjectInfo objectInfo, bool passableness)
            : base(graphicObjectsPack, objectInfo, passableness, true, true) { }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                Program.L.GetMap<Background>().AddObject(this);
                Program.P.AddRenderObject(GraphicObjectsPack);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return Program.L.GetMap<Background>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            Program.L.GetMap<Background>().RemoveObject(PartLayer, Coord);
            Program.P.RemoveRenderObject(GraphicObjectsPack);
        }
    }
}
