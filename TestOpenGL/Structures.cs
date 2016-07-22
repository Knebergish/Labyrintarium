using System;

using TestOpenGL.Logic;
using TestOpenGL.Renders;
using TestOpenGL.VisualObjects;

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
    
        
    /*struct RenderObject
    {
        Texture texture;
        Coord c;
        double zIndex;

        public RenderObject(PhisicalObject vo, double zShift) : this(vo.Texture, vo.C, zShift) { }
        public RenderObject(Texture texture, Coord c, double zShift)
        {
            this.texture = texture;
            this.c = c;
            zIndex = zShift + c.Z;
        }

        public Texture Texture
        { get { return texture; } }
        public Coord C
        { get { return c; } }
        public double ZIndex
        { get { return zIndex; } }
    }*/

    /// <summary>
    /// Структура для передачи координат.
    /// </summary>
    struct Coord// : IComparable
    {
        // Координата (0, 0) - левый нижний угол. Ось X - горизонтальная.
        private int x;
        private int y;
        //-------------


        public Coord(UnsafeCoord uc)
            : this(uc.X, uc.Y) { }
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
            if (!Analytics.CorrectCoordinate(x, y))
                throw new Exception("Неверненькие координатки пришли. (" + x + ", " + y + ")");
        }

        public int X
        { get { return x; } }

        public int Y
        { get { return y; } }
        //=============


        public static bool operator == (Coord C1, Coord C2)
        {
            return C1.X == C2.X && C1.Y == C2.Y ? true : false;
        }
        public static bool operator !=(Coord C1, Coord C2)
        {
            return C1.X != C2.X || C1.Y != C2.Y ? true : false;
        }
        /*public int CompareTo(object obj)
        {
            Coord c = (Coord)obj;
            return z > c.z ? 1 : (z < c.z ? -1 : 0);
        }*/
    }

    struct UnsafeCoord
    {
        private int x;
        private int y;
        //-------------
        

        public UnsafeCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        { get { return x; } }

        public int Y
        { get { return y; } }
        //=============


        public bool IsCorrect()
        { return Analytics.CorrectCoordinate(x, y); }
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
    /// Перечисление типов визуальных объектов (отображаемых на карте). Порядок важен.
    /// </summary>
    enum TypeVisualObject { Background, Block, Being, Item, Decal };

    //ТЕСТЫТЕСТЫТЕСТЫТЕСТЫ///////////////////////////////////////////////////////
    enum Layer { Background, Block, Being, Item, Decal };
    enum ModifyDepth { None, UnderLayer, ToLayer, UnderPartLayer, ToPartLayer }
    struct Position
    {
        Layer layer;
        int partLayer;
        Coord c;


        public Layer Layer
        {
            get
            {
                return layer;
            }
        }

        public int PartLayer
        {
            get
            {
                return partLayer;
            }
        }

        public Coord C
        {
            get
            {
                return c;
            }
        }
        //=============


    }
    //////////////////////////////////////////////////////////////////////////////




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
