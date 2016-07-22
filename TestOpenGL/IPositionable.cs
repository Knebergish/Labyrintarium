using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    interface IPositionable
    {
        int PartLayer { get; }
        Coord Coord { get; }

        bool SetNewPosition(int newPartLayer, Coord newCoord);
    }
}
