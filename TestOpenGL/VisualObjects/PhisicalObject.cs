using System;

namespace TestOpenGL
{
    abstract class PhisicalObject : IPositionable
    {
        int partLayer;
        Coord coord;
        Events events;

        protected delegate bool NewPositionCheckDelegate(int partLayer, Coord coord);
        protected NewPositionCheckDelegate NewPositionCheck;
        //-------------


        protected PhisicalObject()
        {
            NewPositionCheck += IsEmptyPosition;
            events = new Events();
        }

        public int PartLayer
        { get { return partLayer; } }

        public Coord Coord
        { get { return coord; } }

        public Events Events
        { get { return events; } }
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
                events.ChangeCoord();
            }

            return flag;
        }

        protected abstract bool IsEmptyPosition(int partLayer, Coord coord);
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
