using System;

using TestOpenGL.Logic;


namespace TestOpenGL
{
    public delegate void VoidEventDelegate();
    public delegate void BoolEventDelegate(bool value);
    public delegate void IntEventDelegate(int value);
    public delegate void TEventDelegate<T>(T t);
    public delegate void ADelegate<A>(A a);
    public delegate A ABDelegate<A, B>(B b);


    struct Coord
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
                ExceptionAssistant.NewException(new Exception("Неверненькие координатки пришли. (" + x + ", " + y + ")"));
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

    struct Position
    {
        Layer layer;
        int partLayer;
        Coord coord;
        //-------------

        
        public Position(Layer layer)
        {
            this.layer = layer;
            partLayer = 0;
            coord = new Coord();
        }
        public Position(Layer layer, int partLayer, Coord coord)
            : this(layer)
        {
            SetNewPartLayer(partLayer);
            this.coord = coord;
        }

        public Layer Layer
        { get { return layer; } }
        public int PartLayer
        { get { return partLayer; } }
        public Coord Coord
        {
            get { return coord; }
            set { coord = value; }
        }
        //=============


        public void SetNewPartLayer(int newPartLayer)
        {
            if (Program.L.GetDepthLayer(layer) > newPartLayer)
            {
                partLayer = newPartLayer;
                return;
            }
            ExceptionAssistant.NewException(new Exception($"Некорректная часть слоя! Слой: {layer.ToString()}, часть слоя: {partLayer}, новая часть слоя: {newPartLayer}, координаты: {coord.X} {coord.Y}"));
        }
    }
    


    /// <summary>
    /// Части тела, которые может занимать одетый предмет.
    /// </summary>
    enum Part { Head, Body, Leg, LHand, RHand };

    /// <summary>
    /// Секции инвентаря, занимаемые предметом.
    /// </summary>
    enum Section { NoEquipable, Armor, Weapon, Shield }

    /// <summary>
    /// Характеристики, которые имеет сущность.
    /// </summary>
    enum Feature { Power, Coordination, MMR, Stamina, Agility, Sense };

    /// <summary>
    /// Слои отрисовки в порядке отрисовки.
    /// </summary>
    enum Layer { Background, Block, Being, Item, Decal };

    /// <summary>
    /// Модификаторы вычисления глубины размещения текстуры.
    /// </summary>
    enum ModifyDepth { None, UnderLayer, ToLayer, UnderPartLayer, ToPartLayer }

    /// <summary>
    /// Изменять ли часть слоя текстуры при изменении оного у физического объекта.
    /// </summary>
    enum ChangePartLayer { No, Yes }

    /// <summary>
    /// Направления движения сущности.
    /// </summary>
    enum Direction { Left, Up, Right, Down, None };

    /// <summary>
    /// Типы проверки проходимости/проницаемости ячейки.
    /// </summary>
    enum Permeability { Block, BlockAndBeing };

    /// <summary>
    /// Дополнительно нажатые клавиши управления.
    /// </summary>
    enum AdditionalKeys { None, Shift, Ctrl, Alt};
}
