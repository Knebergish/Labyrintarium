using System;
using System.Diagnostics;
using System.Collections.Generic;

using TestOpenGL;

namespace TestOpenGL.Logic
{
    // Проверку корректности координат провожу вручную, т.к. множественные срабатывания исключений конкретно тормозят алгоритм. Знаю, что костыль, но пока не придумал, как сделать лучше.
    class Analytics
    {
        //TODO: почему я смотрю на это, и мне хочется плакать?..
        /// <summary>
        /// Проверка переданных координат на корректность.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool CorrectCoordinate(int x, int y)
        {
            if (x >= 0 && x < Program.L.LengthX)
                if (y >= 0 && y < Program.L.LengthY)
                    return true;
            return false;
        }
        public static bool CorrectCoordinate(int x, int y, int z)
        {
            if (x >= 0 && x < Program.L.LengthX)
                if (y >= 0 && y < Program.L.LengthY)
                    if (z >= 0 && z < Program.L.LengthZ)
                        return true;
            return false;
        }
        public static bool CorrectCoordinate(Coord C)
        {
            if (C.X >= 0 && C.X < Program.L.LengthX)
                if (C.Y >= 0 && C.Y < Program.L.LengthY)
                    if (C.Z >= 0 && C.Z < Program.L.LengthZ)
                        return true;
            return false;

        }





        /// <summary>
        /// Поиск пути в ширину.
        /// </summary>
        /// <param name="start"> Стартовые координаты.</param>
        /// <param name="end"> Координаты точки, к которой ищем путь.</param>
        /// <param name="type"> Тип проходимости ячейки: true - учитывются сущности.</param>
        /// <returns></returns>
        static public Stack<Coord> BFS(Coord start, Coord end)//, bool type)
        {
            //Stopwatch SW = new Stopwatch();
            //SW.Start();
            Stack<Coord> SC = new Stack<Coord>();
            int[] dx = { 1, 0, -1, 0 }, dy = { 0, 1, 0, -1 };
            int[,] grid = new int[Program.L.LengthX, Program.L.LengthY];

            for (int i = 0; i < Program.L.LengthX; i++)
                for (int j = 0; j < Program.L.LengthY; j++)
                    grid[i, j] = -1;

            int d = 0;
            bool stope;
            grid[start.X, start.Y] = 0;

            do
            {
                stope = true;
                for (int xd = 0; xd < Program.L.LengthX; xd++)
                {
                    for (int yd = 0; yd < Program.L.LengthY; yd++)
                    {
                        if (grid[xd, yd] == d)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (yd + dy[k] >= 0 && yd + dy[k] < Program.L.LengthY && xd + dx[k] >= 0 && xd + dx[k] < Program.L.LengthX)//(Program.L.CorrectCoordinate(xd + dx[k], yd + dy[k]))
                                {
                                    //try
                                    //{
                                    if (Program.L.IsPassable(new Coord(xd + dx[k], yd + dy[k])/*, type*/) == true || (xd + dx[k] == end.X && yd + dy[k] == end.Y))
                                    {
                                        if (grid[xd + dx[k], yd + dy[k]] == -1)
                                        {
                                            stope = false;
                                            grid[xd + dx[k], yd + dy[k]] = d + 1;
                                        }
                                    }
                                }
                                //catch (Exception e) { }
                                //}
                            }
                        }
                    }
                }
                d++;
            } while (stope == false && grid[end.X, end.Y] == -1);

            if (grid[end.X, end.Y] == -1)
                return SC;

            int xdop = end.X;
            int ydop = end.Y;
            while (d > 0)
            {
                SC.Push(new Coord(xdop, ydop));
                d--;
                for (int k = 0; k < 4; k++)
                {
                    //try
                    //{
                    if ((ydop + dy[k] >= 0 && ydop + dy[k] < Program.L.LengthY && xdop + dx[k] >= 0 && xdop + dx[k] < Program.L.LengthX))//new Coord(xdop + dx[k], ydop + dy[k]);//
                        if (grid[xdop + dx[k], ydop + dy[k]] == d)
                        {
                            xdop = xdop + dx[k];
                            ydop = ydop + dy[k];
                            break;
                        }
                    //}
                    //catch(Exception e){}

                }
            }
            //SW.Stop();
            //System.Windows.Forms.MessageBox.Show(SW.ElapsedMilliseconds.ToString());

            return SC;
        }

        static public Queue<Coord> CoordsLine(Coord start, Coord end)
        {
            Queue<Coord> coords = new Queue<Coord>(); ;
            int dx, dy;
            int lengthX, lengthY, length;

            dx = end.X - start.X >= 0 ? 1 : -1;
            dy = end.Y - start.Y >= 0 ? 1 : -1;

            lengthX = Math.Abs(end.X - start.X);
            lengthY = Math.Abs(end.Y - start.Y);

            length = lengthX > lengthY ? lengthX : lengthY;

            if (length == 0)
            {
                coords.Enqueue(start);
                return coords;
            }



            int X, Y, d;

            X = start.X;
            Y = start.Y;
            d = -lengthX;

            length++;
            while (length > 0)
            {
                length--;
                coords.Enqueue(new Coord(X, Y));
                if (lengthY <= lengthX)
                {
                    X = X + dx;
                    d = d + 2 * lengthY;
                    if (d > 0)
                    {
                        d = d - 2 * lengthX;
                        Y = Y + dy;
                    }
                }
                else
                {
                    Y = Y + dy;
                    d = d + 2 * lengthX;
                    if (d > 0)
                    {
                        d = d - 2 * lengthY;
                        X = X + dx;
                    }
                }
            }

            return coords;
        }

        static public int Distance(Coord start, Coord end)
        {
            int dX = Math.Abs(start.X - end.X);
            int dY = Math.Abs(start.Y - end.Y);
            return dX > dY ? dX : dY;
        }

        static public bool IsInCamera(Coord C, Camera camera)
        {
            if (C.X < camera.MinX)
                return false;
            if (C.X > camera.MaxX)
                return false;
            if (C.Y < camera.MinY)
                return false;
            if (C.Y > camera.MaxY)
                return false;
            return true;
        }

        static public bool IsInArea(Coord C1, Coord C2, Coord C)
        {
            bool inX = false, inY = false;
            if (C1.X >= C2.X)
            { 
                if (C.X <= C1.X && C.X >= C2.X)
                    inX = true;
            }
            else
            {
                if (C.X <= C2.X && C.X >= C1.X)
                    inX = true;
            }

            if (C1.Y >= C2.Y)
            {
                if (C.Y <= C1.Y && C.Y >= C2.Y)
                    inY = true;
            }
            else
            {
                if (C.Y <= C2.Y && C.Y >= C1.Y)
                    inY = true;
            }

            return inX && inY;
        }
    }
}
