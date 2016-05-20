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

            //aimDecal.texture = Program.TA.GetTexture(TypeVisualObject.Decal, 1);

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
            if (aimCoord.X < camera.ShiftX)
                aimCoord = new Coord(camera.ShiftX, aimCoord.Y);
            if (aimCoord.X > camera.Width + camera.ShiftX - 1)
                aimCoord = new Coord(camera.Width + camera.ShiftX - 1, aimCoord.Y);
            if (aimCoord.Y < camera.ShiftY)
                aimCoord = new Coord(aimCoord.X, camera.ShiftY);
            if (aimCoord.Y > camera.Height + camera.ShiftY - 1)
                aimCoord = new Coord(aimCoord.X, camera.Height + camera.ShiftY - 1);
        }
    }
}
