using TestOpenGL.PhisicalObjects;


namespace TestOpenGL.OutInfo
{
    class Sight : Decal
    {
        public Sight()
            : base(GlobalData.OB.GetDecal(1))
        {
            NewPositionCheck += (int partLayer, Coord coord) => Logic.Analytics.IsInCamera(coord, GlobalData.WorldData.Camera);
        }
        //=============
        

        public void Move(Direction d)
        {
            int dx = 0, dy = 0;
            switch(d)
            {
                case Direction.Left: dx--; break;
                case Direction.Right: dx++; break;
                case Direction.Up: dy++; break;
                case Direction.Down: dy--; break;
            }

            UnsafeCoord newCoord = new UnsafeCoord(Coord.X + dx, Coord.Y + dy);
            if (newCoord.IsCorrect())
                SetNewPosition(PartLayer, new Coord(newCoord));
        }

        public void Check()
        {
            int newX = Coord.X, newY = Coord.Y;

            if (Coord.X < GlobalData.WorldData.Camera.MinX)
                newX = GlobalData.WorldData.Camera.MinX;
            if (Coord.X > GlobalData.WorldData.Camera.MaxX)
                newX = GlobalData.WorldData.Camera.MaxX;
            if (Coord.Y < GlobalData.WorldData.Camera.MinY)
                newY = GlobalData.WorldData.Camera.MinY;
            if (Coord.Y > GlobalData.WorldData.Camera.MaxY)
                newY = GlobalData.WorldData.Camera.MaxY;

            if(newX != Coord.X || newY != Coord.Y)
                SetNewPosition(PartLayer, new Coord(newX, newY));
        }
    }
}
