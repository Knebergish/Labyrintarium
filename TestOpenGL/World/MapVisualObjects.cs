using System.Collections.Generic;


namespace TestOpenGL.World
{
    class MapVisualObjects<T> where T : PhisicalObject
    {
        List<T> mapT;
        Layer layer;
        //-------------


        public MapVisualObjects(Layer layer)
        {
            this.layer = layer;
            mapT = new List<T>();
        }
        //=============


        public T GetObject(int partLayer, Coord c)
        {
            foreach (T t in mapT)
                if (t.PartLayer == partLayer && t.Coord == c)
                    return t;
            return null;
        }
        public List<T> GetCellObject(Coord C)
        {
            List<T> lt = new List<T>();
            T t;
            for (int i = 0; i < Program.L.GetDepthLayer(layer); i++)
            {
                t = GetObject(i, new Coord(C.X, C.Y));
                if (t != null)
                    lt.Add(t);
            }
            return lt;
        }

        public List<T> GetAllObject()
        {
            return new List<T>(mapT);
        }

        public void AddObject(T t)
        {
            mapT.Add(t);
        }

        public void RemoveObject(int partLayer, Coord coord)
        {
            T t = GetObject(partLayer, coord);
            if (t != null)
                mapT.Remove(t);
        }
    }
}
