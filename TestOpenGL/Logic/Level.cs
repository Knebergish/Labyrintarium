using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using Tao.OpenGl;
using Tao.FreeGlut;
//using Tao.Platform.Windows;
//using Tao.Sdl;
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
        public Level(int LengthX, int LengthY, int LengthZ)
        {
            mapBlocks = new MapBlocks(LengthX, LengthY, LengthZ);
            mapBeings = new MapBeings();
            mapDecals = new MapDecals();
        }



        public int LengthX
        { get { return mapBlocks.LengthX; } }
        public int LengthY
        { get { return mapBlocks.LengthY; } }
        public int LengthZ
        { get { return mapBlocks.LengthZ; } }

        
        
        public void AddDecal(Decal d)
        {
            mapDecals.AddDecal(d);
        }
        public void AddDecals(VisualObjectStructure<Decal> decalStructure)
        {
            mapDecals.AddDecals(decalStructure);
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
    }
}

