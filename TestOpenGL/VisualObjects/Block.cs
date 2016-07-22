namespace TestOpenGL.VisualObjects
{
    class Block: GameObject, IInfoble
    {
        bool passableness;
        bool transparency;
        bool permeability;

        ObjectInfo objectInfo;
        //-------------


        public Block(Block block)
            : this(block.ObjectInfo, block.Passableness, block.Transparency, block.Permeability, block.GraphicsObject) { }
        public Block(ObjectInfo objectInfo, bool passableness, bool transparency, bool permeability, GraphicsObject graphictObject)
            : base(graphictObject)
        {
            this.objectInfo = objectInfo;

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

        public ObjectInfo ObjectInfo
        { get { return objectInfo; } }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                Program.L.GetMap<Block>().AddObject(this);
                Program.P.AddGraphicsObject(GraphicsObject);
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
            Program.P.RemoveGraphicsObject(GraphicsObject);
        }
    }
}
