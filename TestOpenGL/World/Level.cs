using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.World
{
    /// <summary>
    /// Объединяет слои карты.
    /// </summary>
    class Level
    {
        int lengthX, lengthY, lengthZ;
        List<object> mapList;
        //-------------


        public Level(int lengthX, int lengthY, int lengthZ)
        {
            this.lengthX = lengthX;
            this.lengthY = lengthY;
            this.lengthZ = lengthZ;
            mapList = new List<object>();
            mapList.Add(new MapVisualObjects<Background>());
            mapList.Add(new MapVisualObjects<Block>());
            mapList.Add(new MapVisualObjects<Being>());
            mapList.Add(new MapVisualObjects<Decal>()); 
        }

        public int LengthX
        { get { return lengthX; } }
        public int LengthY
        { get { return lengthY; } }
        public int LengthZ
        { get { return lengthZ; } }
        //============


        public MapVisualObjects<T> GetMap<T>() where T : PhisicalObject
        {
            foreach (Object o in mapList)
                if (o is MapVisualObjects<T>)
                    return (MapVisualObjects<T>)o;
            throw new Exception("Потерян слой. Идите ищите.");
        }

        public void Pause(int time)
        {
            System.Threading.Thread.Sleep(time);
        }

        

        /// <summary>
        /// Проверяет ячейку на проходимость для сущностей.
        /// </summary>
        /// <param name="C"></param>
        /// <returns></returns>
        public bool IsPassable(int partLayer, Coord coord) //TODO: Нахрен тут partLayer?
        {
            bool flag = true;

            foreach(Background b in GetMap<Background>().GetCellObject(coord))
            {
                if (b.Passableness == false)
                {
                    flag = false;
                    break;
                }
            }

            foreach(Block b in GetMap<Block>().GetCellObject(coord))
            {
                if (b.Passableness == false)
                {
                    flag = false;
                    break;
                }
            }

            flag = GetMap<Being>().GetObject(partLayer, coord) != null ? false : flag;

            return flag;
        }

        //TODO: Passableness используется в IsPermeable, но не используется в IsPassable? Серьёзно?
        public bool IsPermeable(Coord C, Permeability p)
        {
            bool flag = true;// = mapBlocks.IsPassable(C);

            /*if (p == Permeability.BlockAndBeing)
                if (!mapBeings.IsPassable(C))
                    flag = false;*/

            return flag;
        }

        /*public void MapIfFile()
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
        }*/
    }
}

