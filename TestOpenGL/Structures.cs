using System;
//using System.Collections.Generic;

using TestOpenGL.Logic;
//using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    /// <summary>
    /// Структура для передачи нескольких блоков (или не блоков).
    /// </summary>
    /*struct VisualObjectStructure<T>
    {
        private Queue<T> objects;
        private Queue<Coord> coords;
        public VisualObjectStructure(Queue<T> objects, Queue<Coord> coords)
        {
            this.coords = coords;
            this.objects = objects;
        }
        public void Push(T O, Coord C)
        {
            if (objects == null)
                objects = new Queue<T>();
            if (coords == null)
                coords = new Queue<Coord>();

            objects.Enqueue(O);
            coords.Enqueue(C);
        }
        public T PopObject()
        {
            return objects.Dequeue();
        }
        public Coord PopCoord()
        {
            return coords.Dequeue();
        }
        public int Count
        {
            get { return objects == null ? 0 : objects.Count; }
        }
    }*/
    

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
    public delegate void BoolEventDelegate(bool value);
    public delegate void IntEventDelegate(int value);

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
    enum Direction { Left, Up, Right, Down, None };


    /// <summary>
    /// Перечисление типов проверки проходимости/проницаемости ячейки.
    /// </summary>
    enum Permeability { Block, BlockAndBeing };

    /// <summary>
    /// Перечисление дополнительно нажатых клавиш управления.
    /// </summary>
    enum AdditionalKeys { None, Shift, Ctrl, Alt};
}
