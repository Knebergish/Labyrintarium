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

        public Background GetBackground(Coord C)
        {
            return mapBackgrounds[C.X, C.Y];
        }

        public void SetBackground(Coord C, Background background)
        {
            mapBackgrounds[C.X, C.Y] = background;
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

        public bool IsPassable(Coord C)
        {
            if (mapBackgrounds[C.X, C.Y] != null)
                if (!mapBackgrounds[C.X, C.Y].Passableness)
                    return false;
            return true;
        }
    }
}
