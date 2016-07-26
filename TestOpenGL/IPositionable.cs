namespace TestOpenGL
{
    interface IPositionable
    {
        int PartLayer { get; }
        Coord Coord { get; }

        bool SetNewPosition(int newPartLayer, Coord newCoord);
    }
}
