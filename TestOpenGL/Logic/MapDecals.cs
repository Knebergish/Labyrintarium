using System.Data;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /*class MapDecals
    {
        private DataTable DTDecals;

        public MapDecals()
        {
            DTDecals = new DataTable();
            this.DTDecals.Columns.Add("groupId", System.Type.GetType("System.Int32"));
            this.DTDecals.Columns.Add("decal", typeof(Decal));
        }

        //TODO: Хрень, мне это не нравится. Подумать над переделкой системы декалирования.

        public List<Decal> GetDecals(Coord C)
        {
            List<Decal> ld = new List<Decal>();
            for (int i = 0; i < DTDecals.Rows.Count; i++)
                if (((Decal)DTDecals.Rows[i]["decal"]).C.X == C.X && ((Decal)DTDecals.Rows[i]["decal"]).C.Y == C.Y)
                    ld.Add((Decal)DTDecals.Rows[i]["decal"]);
            return ld;
        }
        public List<Decal> GetAllDecals()
        {
            List<Decal> ld = new List<Decal>();
            for (int i = 0; i < DTDecals.Rows.Count; i++)
                ld.Add((Decal)DTDecals.Rows[i]["decal"]);
            return ld;
        }
        private int NextNumberGroup()
        {
            int nextNumber = -1;
            for (int i = 0; i < DTDecals.Rows.Count; i++)
                nextNumber = (int)DTDecals.Rows[i]["groupId"] > nextNumber ? (int)DTDecals.Rows[i]["groupId"] : nextNumber;
            return ++nextNumber;
        }
        public int AddDecal(Decal d)
        {
            int currentNumberGroup = NextNumberGroup();
            DTDecals.Rows.Add(currentNumberGroup, d);
            return currentNumberGroup;
        }
        /*public int AddDecal(Decal d, Coord C)
        {
            int currentNumberGroup = NextNumberGroup();
            d.C = C;
            DTDecals.Rows.Add(currentNumberGroup, d);
            return currentNumberGroup;
        }*/
        /*public int AddDecals(VisualObjectStructure<Decal> decalStructure)
        {
            Decal d;
            int currentNumberGroup = NextNumberGroup();

            while (decalStructure.Count != 0)
            {
                d = decalStructure.PopObject();
                d.C = decalStructure.PopCoord();
                DTDecals.Rows.Add(currentNumberGroup, d);
            }

            return currentNumberGroup;
        }
        public void RemoveGroupDecals(int numberGroup)
        {
            for (int i = DTDecals.Rows.Count - 1; i >= 0; i--)
                if ((int)DTDecals.Rows[i]["groupId"] == numberGroup)
                    DTDecals.Rows.RemoveAt(i);
        }
        public int CountDecals
        {
            get { return DTDecals.Rows.Count; }
        }
        public void ClearDecals()
        {
            DTDecals.Rows.Clear();
        }
    }*/
}
