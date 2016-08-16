using TestOpenGL.Renders;


namespace TestOpenGL.PhisicalObjects
{
    class Decal : PhisicalObject
    {
        public Decal(Decal decal)
            : this(decal.GraphicObjectsPack) { }
        public Decal(GraphicObjectsPack graphicObjectsPack)
            : base(Layer.Decal, graphicObjectsPack, new ObjectInfo(0, "", "")) { }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                GlobalData.WorldData.Level.GetMap<Decal>().AddObject(this);
                GlobalData.WorldData.RendereableObjectsContainer.Add(GraphicObjectsPack);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            //return GlobalData.WorldData.Level.GetMap<Decal>().GetObject(partLayer, coord) == null ? true : false;
            return true;
        }

        public override void Despawn()
        {
            GlobalData.WorldData.Level.GetMap<Decal>().RemoveObject(PartLayer, Coord);
            GlobalData.WorldData.RendereableObjectsContainer.Remove(GraphicObjectsPack);
        }
    }
}
