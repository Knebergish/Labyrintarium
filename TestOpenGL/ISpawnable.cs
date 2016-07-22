using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    interface ISpawnable
    {
        bool Spawn(int partLayer, Coord coord);
        void Despawn();
    }
}
