using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Sight
    {
        private Coord aimCoord;
        public Decal aimDecal;
        private Camera camera;
        public Sight(Camera camera)
        {
            aimCoord = new Coord(0, 0);
            aimDecal = Program.OB.GetDecal(1);
            this.camera = camera;

            this.camera.changeCameraPosition += Check;
        }

        public Coord AimCoord
        {
            get { return aimCoord; }
            set { aimCoord = value; Check(); }
        }

        public void MoveSight(Direction d)
        {
            int dx = 0, dy = 0;
            switch(d)
            {
                case Direction.Left: dx--; break;
                case Direction.Right: dx++; break;
                case Direction.Up: dy++; break;
                case Direction.Down: dy--; break;
            }

            try
            {
                aimCoord = new Coord(aimCoord.X + dx, aimCoord.Y + dy);
            }
            catch { }
            Check();
        }

        public void Check()
        {
            if (aimCoord.X < camera.MinX)
                aimCoord = new Coord(camera.MinX, aimCoord.Y);
            if (aimCoord.X > camera.MaxX)
                aimCoord = new Coord(camera.MaxX, aimCoord.Y);
            if (aimCoord.Y < camera.MinY)
                aimCoord = new Coord(aimCoord.X, camera.MinY);
            if (aimCoord.Y > camera.MaxY)
                aimCoord = new Coord(aimCoord.X, camera.MaxY);
        }
    }
}
