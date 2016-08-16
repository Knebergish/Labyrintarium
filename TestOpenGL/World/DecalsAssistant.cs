using System;
using System.Collections.Generic;

using TestOpenGL.Logic;
using TestOpenGL.PhisicalObjects;

namespace TestOpenGL.World
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
            GlobalData.WorldData.RendereableObjectsContainer.Add(decal.GraphicObjectsPack);

            int a = nextKey - 1;
            return new Action(() => RemoveDecal(a));
        }

        void RemoveDecal(int key)
        {
            GlobalData.WorldData.RendereableObjectsContainer.Remove(dictionaryDecals[key].GraphicObjectsPack);
            dictionaryDecals.Remove(key);
        }
    }
}
