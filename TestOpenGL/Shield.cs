using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Shield : Item
    {
        public Shield(int id, string name, string description, Texture texture, int level, int price, List<Part> parts)
            : base(id, name, description, texture, level, price, parts)
        { }
    }
}
