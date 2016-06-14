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
    }
}
