using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class MapBackgrounds
    {
        private int lengthX, lengthY;
        private Background [,] mapBackgrounds;

        public MapBackgrounds(int lengthX, int lengthY)
        {
            this.lengthX = lengthX;
            this.lengthY = lengthY;

            mapBackgrounds = new Background[this.lengthX, this.lengthY];
        }

        public Background this[Coord C]
        {
            get { return mapBackgrounds[C.X, C.Y]; }
            set { mapBackgrounds[C.X, C.Y] = value; }
        }
        public void SetBackgrounds(VisualObjectStructure<Background> backgroundsStructure)
        {
            Coord C;

            while (backgroundsStructure.Count > 0)
            {
                C = backgroundsStructure.PopCoord();

                this.mapBackgrounds[C.X, C.Y] = null;
                this.mapBackgrounds[C.X, C.Y] = backgroundsStructure.PopObject();
            }
        }

        public int LengthX
        { get { return lengthX; } }
        public int LengthY
        { get { return lengthY; } }

        public bool IsPassable(Coord C)
        {
            if (mapBackgrounds[C.X, C.Y] != null)
                if (!mapBackgrounds[C.X, C.Y].Passableness)
                    return false;
            return true;
        }
    }
}
