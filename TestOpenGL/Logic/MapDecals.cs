using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /// <summary>
    /// Организует работу с декалями на карте.
    /// </summary>
    class MapDecals
    {
        private List<Decal> listDecals;

        public MapDecals()
        {
            listDecals = new List<Decal>();
        }

        //TODO: Хрень, мне это не нравится. Подумать над переделкой системы декалирования.

        public List<Decal> GetAllDecals()
        {
            return listDecals;
        }
        public void AddDecal(Decal d)
        {
            listDecals.Add(d);
        }
        public void AddDecals(VisualObjectStructure<Decal> decalStructure)
        {
            Decal d;
            while (decalStructure.Count != 0)
            {
                d = decalStructure.PopObject();
                d.C = decalStructure.PopCoord();
                listDecals.Add(d);
            }
        }
        public int CountDecals
        {
            get { return listDecals.Count; }
        }
        public void ClearDecals()
        {
            listDecals.Clear();
        }
    }
}
