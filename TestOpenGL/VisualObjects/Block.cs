namespace TestOpenGL.VisualObjects
{
    class Block: VisualObject
    {
        bool passableness;
        bool transparency;
        bool permeability;


        public Block(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture)
            : base(id, name, description, texture)
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

        public override bool Spawn(Coord C)
        {
            if (SetNewCoord(C))
            {
                Program.L.GetMap<Block>().AddVO(this, C);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyCell(Coord C)
        {
            return Program.L.GetMap<Block>().GetVO(C) == null ? true : false;
        }
    }
}
