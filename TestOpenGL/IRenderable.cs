using System.Collections.Generic;

using TestOpenGL.Renders;


namespace TestOpenGL
{
    interface IRenderable
    {
        List<Cell> GetCells();
        int PartLayer { get; }
        Coord Coord { get; }
    }
}
