using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.DataIO
{
    class MapVisualObjects<T> where T : VisualObject
    {
        List<T> mapT;
        public MapVisualObjects()
        {
            mapT = new List<T>();
        }

        public T GetVO(Coord C)
        {
            foreach (T t in mapT)
                if (t.C == C)
                    return t;
            return null;
        }
        public List<T> GetCellVO(Coord C)
        {
            List<T> lt = new List<T>();
            T t;
            for (int i = 0; i < Program.L.LengthZ; i++)
            {
                t = GetVO(new Coord(C.X, C.Y, i));
                if (t != null)
                    lt.Add(t);
            }
            return lt;
        }
        public List<T> GetAllVO()
        {
            return new List<T>(mapT);
        }

        public bool AddVO(T tvo, Coord C)
        {
            if (tvo.SetNewCoord(C))
            {
                mapT.Add(tvo);
                return true;
            }
            return false;
        }

        public void RemoveVO(Coord C)
        {
            T t = this.GetVO(C);
            if (t != null)
                mapT.Remove(t);
        }
    }
}
