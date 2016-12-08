using System;
using TestOpenGL.Renders;


namespace TestOpenGL.PhisicalObjects
{
    class Block: PhisicalObject
    {
        bool passableness;
        bool transparency;
        bool permeability;
        //-------------


        public Block(Block block)
            : this(block.GraphicObjectsPack, block.ObjectInfo, block.Passableness, block.Transparency, block.Permeability) { }
        public Block(GraphicObjectsPack graphicObjectsPack, ObjectInfo objectInfo, bool passableness, bool transparency, bool permeability)
            : base(Layer.Block, graphicObjectsPack, objectInfo)
        {
            this.passableness = passableness;
            this.transparency = transparency;
            this.permeability = permeability;
        }

        //Проходимость (для сущностей)
        public bool Passableness
        {
            get { return passableness; }
            set { passableness = value; }
        }
        //Прозрачность (для видимости за объектом)
        public bool Transparency
        {
            get { return transparency; }
            set { transparency = value; }
        }
        //Проницаемость (для атаки сквозь такой объект)
        public bool Permeability
        {
            get { return permeability; }
            set { permeability = value; }
        }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                GlobalData.WorldData.Level.GetMap<Block>().AddObject(this);
                GlobalData.WorldData.RendereableObjectsContainer.Add(GraphicObjectsPack);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return GlobalData.WorldData.Level.GetMap<Block>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            GlobalData.WorldData.Level.GetMap<Block>().RemoveObject(PartLayer, Coord);
            GlobalData.WorldData.RendereableObjectsContainer.Remove(GraphicObjectsPack);
        }

		public override PhisicalObject Clone()
		{
			return new Block(this);
		}
	}
}
