using System;
using System.Data;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    abstract class PhisicalObject : IPositionable, ISpawnable, IInfoble
    {
        int partLayer;
        Coord coord;
        Events events;
        GraphicObjectsPack graphicObjectsPack;
        ObjectInfo objectInfo;

        protected delegate bool NewPositionCheckDelegate(int partLayer, Coord coord);
        protected NewPositionCheckDelegate NewPositionCheck;
        //-------------


        private PhisicalObject()
        {
            NewPositionCheck += IsEmptyPosition;
            events = new Events();
        }
        public PhisicalObject(GraphicObjectsPack graphicObjectsPack, ObjectInfo objectInfo)
            : this()
        {
            this.graphicObjectsPack = graphicObjectsPack ?? new GraphicObjectsPack();
            graphicObjectsPack.PositionObject = this;

            this.objectInfo = objectInfo;
        }

        public int PartLayer
        { get { return partLayer; } }

        public Coord Coord
        { get { return coord; } }

        public Events Events
        { get { return events; } }

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
                partLayer = newPartLayer;
                coord = newCoord;

                graphicObjectsPack.UpdatePosition();

                events.ChangeCoord();
            }

            return flag;
        }
        protected abstract bool IsEmptyPosition(int partLayer, Coord coord);

        public abstract bool Spawn(int partLayer, Coord coord);
        public abstract void Despawn();
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
