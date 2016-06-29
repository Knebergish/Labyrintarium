using System;
//using System.Collections.Generic;

using TestOpenGL.Logic;
//using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    /// <summary>
    /// Структура для передачи координат.
    /// </summary>
    struct Coord
    {
        // Координата (0, 0) - левый нижний угол. Ось X - горизонтальная.
        private int x;
        private int y;
        private int z;

        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        public int Z
        {
            get { return z; }
        }

        public Coord(int x, int y)
            : this(x, y, 0)
        { }
        public Coord(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            if (!Analytics.CorrectCoordinate(x, y, z))
                throw new Exception("Неверненькие координатки пришли. (" + x + ", " + y + ", " + z +")");
        }
        public static bool operator == (Coord C1, Coord C2)
        {
            return C1.X == C2.X && C1.Y == C2.Y && C1.Z == C2.Z ? true : false;
        }
        public static bool operator !=(Coord C1, Coord C2)
        {
            return C1.X != C2.X || C1.Y != C2.Y || C1.Z != C2.Z ? true : false;
        }
    }


    public delegate void VoidEventDelegate();
    public delegate void IntEventDelegate(int value);
    public delegate void BoolEventDelegate(bool value);

    /// <summary>
    /// Перечисление частей тела, которые может занимать одетый предмет.
    /// </summary>
    enum Part { Head, Body, Leg, LHand, RHand };


    /// <summary>
    /// Перечисление характеристик, которые имеет сущность.
    /// </summary>
    enum Feature { Power, Coordination, MMR, Stamina, Agility, Sense };


    /// <summary>
    /// Перечисление типов визуальных объектов (отображаемых на карте).
    /// </summary>
    enum TypeVisualObject { Background, Block, Being, Item, Decal };


    /// <summary>
    /// Перечисление направлений движения сущности.
    /// </summary>
    enum Direction { Left, Up, Right, Down, None};


    /// <summary>
    /// Перечисление типов проверки проходимости/проницаемости ячейки.
    /// </summary>
    enum Permeability { Block, BlockAndBeing };
}
