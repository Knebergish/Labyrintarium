using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    abstract class UsedBlock : Block
    {
        public UsedBlock(int id, string name, string description, bool passableness, bool transparency, bool permeability, Texture texture) 
            : base(id, name, description, passableness, transparency, permeability, texture)
        {
        }

        public abstract void Use();
    }
}
