using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL.VisualObjects
{
    class Background:Block
    {
        public Background(int id, string name, string description, bool passableness, Texture texture)
            : base(id, name, description, passableness, true, true, texture)
        {
        }

        public override bool Spawn(Coord C)
        {
            if (SetNewCoord(C))
            {
                Program.L.GetMap<Background>().AddVO(this, C);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyCell(Coord C)
        {
            return Program.L.GetMap<Background>().GetVO(C) == null ? true : false;
        }
    }
}
