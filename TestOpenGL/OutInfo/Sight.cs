using TestOpenGL.Renders;
using TestOpenGL.PhisicalObjects;

namespace TestOpenGL.OutInfo
{
    class Sight
    {
        Coord coord;
        //Decal aimDecal;
        GraphicObject graphicObject;
        Camera camera;
        EventSight eventSight;
        //-------------


        public Sight(Camera camera)
        {
            //coord = new Coord(0, 0);
            graphicObject = GlobalData.OB.GetGraphicObject(6, Layer.Decal);
            this.camera = camera;
            eventSight = new EventSight();
            
            this.camera.ChangePositionEvent += Check;
            //GlobalData.WorldData.RendereableObjectsContainer.Add(graphicObject);
            //GlobalData.WorldData.RendereableObjectsContainer.Add(graphicObject);
        }
        
        public Coord Coord
        {
            get { return coord; }
            set
            {
                coord = value;
                graphicObject.SetNewPosition(0, coord);
                Check();
                eventSight.SightChangeCoord();
            }
        }

        internal EventSight EventSight
        { get { return eventSight; } }

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

            if (Logic.Analytics.CorrectCoordinate(coord.X + dx, coord.Y + dy))
                Coord = new Coord(coord.X + dx, coord.Y + dy);
        }

        public void Check()
        {
            if (coord.X < camera.MinX)
                Coord = new Coord(camera.MinX, coord.Y);
            if (coord.X > camera.MaxX)
                Coord = new Coord(camera.MaxX, coord.Y);
            if (coord.Y < camera.MinY)
                Coord = new Coord(coord.X, camera.MinY);
            if (coord.Y > camera.MaxY)
                Coord = new Coord(coord.X, camera.MaxY);
        }
    }

    class EventSight
    {
        public event VoidEventDelegate EventSightChangeCoord;
        public void SightChangeCoord()
        {
            EventSightChangeCoord?.Invoke();
        }
    }
}
