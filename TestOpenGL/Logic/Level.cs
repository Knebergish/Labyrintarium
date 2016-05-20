using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.DevIl;

using TestOpenGL;
using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    /// <summary>
    /// Объединяет слои карты.
    /// </summary>
    class Level
    {
        private MapBlocks mapBlocks;
        private MapBeings mapBeings;
        private MapDecals mapDecals;
        
        /// <param name="LengthX"> Ширина игровой карты.</param>
        /// <param name="LengthY"> Выстока игровой карты.</param>
        /// <param name="LengthZ"> Глубина игровой карты.</param>
        public Level(int lengthX, int lengthY, int lengthZ)
        {
            mapBlocks = new MapBlocks(lengthX, lengthY, lengthZ);
            mapBeings = new MapBeings();
            mapDecals = new MapDecals();
        }



        public int LengthX
        { get { return mapBlocks.LengthX; } }
        public int LengthY
        { get { return mapBlocks.LengthY; } }
        public int LengthZ
        { get { return mapBlocks.LengthZ; } }

        
        
        public int AddDecal(Decal d)
        {
            return mapDecals.AddDecal(d);
        }
        public int AddDecals(VisualObjectStructure<Decal> decalStructure)
        {
            return mapDecals.AddDecals(decalStructure);
        }
        public void RemoveGroupDecals(int numberGroup)
        {
            mapDecals.RemoveGroupDecals(numberGroup);
        }
        public int CountDecals
        {
            get { return mapDecals.CountDecals; }
        }
        public void ClearDecals()
        {
            mapDecals.ClearDecals();
        }
        public List<Decal> GetAllDecals()
        {
            return mapDecals.GetAllDecals();
        }
        

        public Block GetBlock(Coord C)
        {
            return mapBlocks.GetBlock(C);
        }
        public void SetBlock(Block B, Coord C)
        {
            mapBlocks.SetBlock(B, C);
        }
        public void SetBlocks(VisualObjectStructure<Block> blocksStructure)
        {
            mapBlocks.SetBlocks(blocksStructure);
        }
        


        public Being GetBeing(Coord C)
        {
            return mapBeings.GetBeing(C);
        }
        public Being GetBeing(int num)
        {
            return mapBeings.GetBeing(num);
        }
        public List<Being> GetAllBeings()
        {
            return mapBeings.GetAllBeings();
        }
        public bool AddBeing(Being B)
        {
            return mapBeings.AddBeing(B);
        }
        public void RemoveBeing(Coord C)
        {
            mapBeings.RemoveBeing(C);
        }
        public int CountBeings
        {
            get { return mapBeings.CountBeings; }
        }
        public void ClearDeadBeings()
        {
            mapBeings.ClearDeadBeings();
        }

        
        /// <summary>
        /// Проверяет ячейку на проходимость для сущностей.
        /// </summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public bool IsPassable(Coord C)
        {
            bool flag = mapBlocks.IsPassable(C);

            if (!mapBeings.IsPassable(C))
                flag = false;

            return flag;
        }

        public bool IsPermeable(Coord C, Passableness p)
        {
            bool flag = mapBlocks.IsPassable(C);

            if(p==Passableness.BlockAndBeing)
                if (!mapBeings.IsPassable(C))
                    flag = false;

            return flag;
        }

        public void MapIfFile()
        {
            StreamWriter sw = new StreamWriter("fil.txt");

            sw.WriteLine(this.LengthX.ToString() + " " + this.LengthY.ToString() + " " + this.LengthZ.ToString());

            for (int i = 0; i < this.LengthX; i++)
                for (int j = 0; j < this.LengthY; j++)
                    for (int l = 0; l < this.LengthZ; l++)
                        if (this.GetBlock(new Coord(i, j, l)) != null)
                            sw.WriteLine(i.ToString() + " " + j.ToString() + " " + l.ToString() + " " + this.GetBlock(new Coord(i, j, l)).Id.ToString());

            sw.Close();
        }

        public void FileInMap()
        {
            StreamReader sr = new StreamReader("fil.txt");
            string[] s;

            s = sr.ReadLine().Split(' ');
            this.mapBlocks = new MapBlocks(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
            
            while(!sr.EndOfStream)
            {
                s = sr.ReadLine().Split(' ');
                this.SetBlock(Program.OB.GetBlock(int.Parse(s[3])), new Coord(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }
            sr.Close();

        }
    }
}

