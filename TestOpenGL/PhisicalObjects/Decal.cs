using System;
using TestOpenGL.Renders;


namespace TestOpenGL.PhisicalObjects
{
    class Decal : PhisicalObject
    {
        Action thisRemover;
        //-------------
        public Decal(Decal decal)
            : this(decal.GraphicObjectsPack) { }
        public Decal(GraphicObjectsPack graphicObjectsPack)
            : base(Layer.Decal, graphicObjectsPack, new ObjectInfo(0, "", "")) { }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                //GlobalData.WorldData.Level.GetMap<Decal>().AddObject(this);
                //GlobalData.WorldData.RendereableObjectsContainer.Add(GraphicObjectsPack);
                thisRemover = GlobalData.WorldData.DecalsAssistant.AddDecal(this, coord);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return true;
        }

        public override void Despawn()
        {
            //GlobalData.WorldData.Level.GetMap<Decal>().RemoveObject(PartLayer, Coord);
            //GlobalData.WorldData.RendereableObjectsContainer.Remove(GraphicObjectsPack);
            thisRemover();
        }

		public override PhisicalObject Clone()
		{
			return new Decal(this);
		}
	}
}
