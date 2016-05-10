using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class MapBeings
    {
        private List<Being> listBeings;

        public MapBeings()
        {
            listBeings = new List<Being>();
        }

        //TODO: можно проверять просто наличие сущности в ячейке, но можно и отдельным методом
        public bool IsPassable(Coord C)
        {
            if (GetBeing(C) != null)
                return false;
            return true;
        }


        //TODO: Даже не знаю, должны ли быть эти методы тут, а если и должны, то в таком ли виде?
        /// <summary>
        /// Поиск сущности по переданным координатам в массиве сущностей (возвращает только живых)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Being GetBeing(Coord C)
        {
            for (int i = 0; i < listBeings.Count; i++)
                if ((listBeings[i].C.X == C.X && listBeings[i].C.Y == C.Y) && listBeings[i].isSpawned)
                    return listBeings[i];
            return null;
        }
        public Being GetBeing(int num)
        {
            if (num >= 0 && num < this.listBeings.Count)
                return this.listBeings[num];
            return null;
        }
        public List<Being> GetAllBeings()
        {
            return listBeings;
        }
        /// <summary>
        /// Добавление сущности в очередь.
        /// </summary>
        /// <param name="B"> Объект сущности (обязательны корректные координаты в ней)</param>
        public bool AddBeing(Being B)
        {
            if (this.GetBeing(B.C) == null)
            {
                this.listBeings.Add(B);
                return true;
            }
            else return false;
        }
        public void RemoveBeing(Coord C)
        {
            this.listBeings.Remove(this.GetBeing(C));
        }
        public int CountBeings
        {
            get { return this.listBeings.Count; }
        }
        public void ClearDeadBeings()
        {
            for (int i = listBeings.Count - 1; i >= 0; i--)
            {
                if (!listBeings[i].isSpawned)
                    listBeings.RemoveAt(i);
            }
        }
    }
}
