using TestOpenGL.Renders;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.OutInfo
{
    class Sight
    {
        private Coord c;
        private Decal aimDecal;
        private Camera camera;
        //-------------


        public Sight(Camera camera)
        {
            c = new Coord(0, 0);
            aimDecal = Program.OB.GetDecal(1);
            this.camera = camera;

            this.camera.changeCameraPosition += Check;
        }

        public Decal AimDecal
        {
            get { return aimDecal; }
        }
        
        public Coord C
        {
            get { return c; }
            set { c = value; Check(); }
        }
        //=============


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

            if (Logic.Analytics.CorrectCoordinate(c.X + dx, c.Y + dy))
                c = new Coord(c.X + dx, c.Y + dy);
            
            Check();
        }

        public void Check()
        {
            if (c.X < camera.MinX)
                c = new Coord(camera.MinX, c.Y);
            if (c.X > camera.MaxX)
                c = new Coord(camera.MaxX, c.Y);
            if (c.Y < camera.MinY)
                c = new Coord(c.X, camera.MinY);
            if (c.Y > camera.MaxY)
                c = new Coord(c.X, camera.MaxY);
        }
    }
}
