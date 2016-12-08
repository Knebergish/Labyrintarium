using System;

using TestOpenGL.Renders;
using TestOpenGL.PhisicalObjects;


namespace TestOpenGL
{
    abstract class PhisicalObject : IPositionable, ISpawnable, IInfoble
    {
        Position position;
        GraphicObjectsPack graphicObjectsPack;
        ObjectInfo objectInfo;

        protected delegate bool NewPositionCheckDelegate(int partLayer, Coord coord);
        protected NewPositionCheckDelegate NewPositionCheck;
        event VoidEventDelegate changeCoordEvent;
        //-------------


        private PhisicalObject(Layer layer)
        {
            position = new Position(layer);
            NewPositionCheck += IsEmptyPosition;
        }
        public PhisicalObject(Layer layer, GraphicObjectsPack graphicObjectsPack, ObjectInfo objectInfo)
            : this(layer)
        {
            this.graphicObjectsPack = graphicObjectsPack ?? new GraphicObjectsPack();
            graphicObjectsPack.PositionObject = this;

            this.objectInfo = objectInfo;
        }

        public int PartLayer
        { get { return position.PartLayer; } }

        public Coord Coord
        { get { return position.Coord; } }

        public event VoidEventDelegate ChangeCoordEvent
        {
            add { changeCoordEvent += value; }
            remove { changeCoordEvent -= value; }
        }

        public ObjectInfo ObjectInfo
        { get { return objectInfo; } }

        public GraphicObjectsPack GraphicObjectsPack
        {
            get  { return graphicObjectsPack; }
        }
        //=============


        public bool SetNewPosition(int newPartLayer, Coord newCoord)
        {
            bool flag = true;

            foreach(Delegate d in NewPositionCheck.GetInvocationList())
            {
                flag = ((NewPositionCheckDelegate)d)(newPartLayer, newCoord) == true ? flag : false;
            }

            if (flag)
            {
                position.SetNewPartLayer(newPartLayer);
                position.Coord = newCoord;

                graphicObjectsPack.UpdatePosition();

                changeCoordEvent?.Invoke();
            }

            return flag;
        }
        protected abstract bool IsEmptyPosition(int partLayer, Coord coord);

        public abstract bool Spawn(int partLayer, Coord coord);
        public abstract void Despawn();

		public abstract PhisicalObject Clone();
    }



    class Events
    {
        public event VoidEventDelegate EventChangeCoord;
        public void ChangeCoord()
        {
            EventChangeCoord?.Invoke();
        }
    }
}
