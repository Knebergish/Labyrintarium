using TestOpenGL.Renders;


namespace TestOpenGL.PhisicalObjects
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
                GlobalData.WorldData.Level.GetMap<Background>().AddObject(this);
                GlobalData.WorldData.RendereableObjectsContainer.Add(GraphicObjectsPack);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return GlobalData.WorldData.Level.GetMap<Background>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            GlobalData.WorldData.Level.GetMap<Background>().RemoveObject(PartLayer, Coord);
            GlobalData.WorldData.RendereableObjectsContainer.Remove(GraphicObjectsPack);
        }
    }
}
