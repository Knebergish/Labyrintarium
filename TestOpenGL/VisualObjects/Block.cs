using System;
using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    class Block: VisualObject, IInfoble
    {
        bool passableness;
        bool transparency;
        bool permeability;

        ObjectInfo objectInfo;
        //-------------


        public Block(Block block)
            : this(block.ObjectInfo.Id, block.ObjectInfo.Name, block.ObjectInfo.Description, block.Passableness, block.Transparency, block.Permeability, block.Texture) { }
        public Block(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture)
            : base(texture)
        {
            this.passableness = passableness;
            this.transparency = transparency;
            this.permeability = permeability;

            objectInfo = new ObjectInfo(id, name, description);
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
