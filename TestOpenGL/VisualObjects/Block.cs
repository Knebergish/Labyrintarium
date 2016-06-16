namespace TestOpenGL.VisualObjects
{
    class Block: VisualObject
    {
        bool passableness;
        bool transparency;
        bool permeability;

        //Проходимость (для сущностей)
        public bool Passableness { get { return passableness; } }
        //Прозрачность (для видимости за объектом)
        public bool Transparency { get { return transparency; } }
        //Проницаемость (для атаки сквозь такой объект)
        public bool Permeability { get { return permeability; } }

        public Block(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture)
            : base(id, name, description, texture)
        {
            this.passableness = passableness;
            this.transparency = transparency;
            this.permeability = permeability;
        }
    }
}
