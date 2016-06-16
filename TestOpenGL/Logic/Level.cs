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
        private int lengthX, lengthY, lengthZ; //TODO: Эти хрени где только не хранятся. Т.е. суть дублирование.
        private MapBackgrounds mapBackgrounds;
        private MapBlocks mapBlocks;
        private MapBeings mapBeings;
        private MapDecals mapDecals;

        public MapBackgrounds MapBackgrounds
        { get { return mapBackgrounds; } }
        public MapBlocks MapBlocks
        { get { return mapBlocks; } }
        public MapBeings MapBeings
        { get { return mapBeings; } }
        public MapDecals MapDecals
        { get { return mapDecals; } }
        
        /// <param name="LengthX"> Ширина игровой карты.</param>
        /// <param name="LengthY"> Выстока игровой карты.</param>
        /// <param name="LengthZ"> Глубина игровой карты.</param>
        public Level(int lengthX, int lengthY, int lengthZ)
        {
            mapBackgrounds = new MapBackgrounds(lengthX, lengthY);
            mapBlocks = new MapBlocks(lengthX, lengthY, lengthZ);
            mapBeings = new MapBeings();
            mapDecals = new MapDecals();
            this.lengthX = lengthX;
            this.lengthY = lengthY;
            this.lengthZ = lengthZ;
        }


        public void Pause(int time)
        {
            System.Threading.Thread.Sleep(time);
        }


        public int LengthX
        { get { return lengthX; } }
        public int LengthY
        { get { return lengthY; } }
        public int LengthZ
        { get { return lengthZ; } }
        
        /// <summary>
        /// Проверяет ячейку на проходимость для сущностей.
        /// </summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public bool IsPassable(Coord C)
        {
            bool flag;

            flag = mapBackgrounds.IsPassable(C);

            flag = mapBlocks.IsPassable(C) ? flag : false;

            flag = mapBeings.IsPassable(C) ? flag : false;

            return flag;
        }

        //TODO: Passableness используется в IsPermrable, но не используется в IsPassable? Серьёзно?
        public bool IsPermeable(Coord C, Permeability p)
        {
            bool flag = mapBlocks.IsPassable(C);

            if (p == Permeability.BlockAndBeing)
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
                        if (this.mapBlocks.GetBlock(new Coord(i, j, l)) != null)
                            sw.WriteLine(i.ToString() + " " + j.ToString() + " " + l.ToString() + " " + this.mapBlocks.GetBlock(new Coord(i, j, l)).Id.ToString());

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
                this.mapBlocks.SetBlock(Program.OB.GetBlock(int.Parse(s[3])), new Coord(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2])));
            }
            sr.Close();
        }
    }
}

