using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.Renders;

namespace TestOpenGL
{
    class Cell
    {
        Coord c;
        double globalDepth;
        Texture texture;

        public Cell(Coord c, double globalDepth, Texture texture)
        {
            this.c = c;
            this.globalDepth = globalDepth;
            this.texture = texture;
        }

        public Coord C
        {
            get
            {
                return c;
            }

            set
            {
                c = value;
            }
        }

        public double GlobalDepth
        {
            get
            {
                return globalDepth;
            }

            set
            {
                globalDepth = value;
            }
        }

        public Texture Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }
    }
}
