using TestOpenGL.Renders;


namespace TestOpenGL.VisualObjects
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
                Program.L.GetMap<Block>().AddObject(this);
                Program.P.AddRenderObject(GraphicObjectsPack);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return Program.L.GetMap<Block>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            Program.L.GetMap<Block>().RemoveObject(PartLayer, Coord);
            Program.P.RemoveRenderObject(GraphicObjectsPack);
        }
    }
}
