using System;
using System.Collections.Generic;

using TestOpenGL.Logic;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class DecalsAssistant
    {
        int nextKey;
        Dictionary<int, Decal> dictionaryDecals;

        public DecalsAssistant()
        {
            nextKey = 0;

            dictionaryDecals = new Dictionary<int, Decal>();
            dictionaryDecals.Keys.GetEnumerator();

            Func<int, int, int> test1 = (x, y) => (x + y);
            Func<int, int> test2 = test1.Partial(2);
        }

        public Action AddDecal(Decal decal, Coord coord)
        {
            decal.SetNewPosition(0, coord);
            dictionaryDecals.Add(nextKey++, decal);
            Program.P.AddRenderObject(decal.GraphicObjectsPack);

            int a = nextKey - 1;
            return new Action(() => RemoveDecal(a));
        }

        void RemoveDecal(int key)
        {
            Program.P.RemoveRenderObject(dictionaryDecals[key].GraphicObjectsPack);
            dictionaryDecals.Remove(key);
        }
    }
}
