using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    abstract class VisualObject
    {
        Coord c;
        Texture texture;
        EventsVisualObject eventsVO;
        //-------------


        protected VisualObject(Texture texture)
        {
            this.texture = texture;
            eventsVO = new EventsVisualObject();
        }

        public Coord C
        { get { return c; } }

        public Texture Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public EventsVisualObject EventsVO
        { get { return eventsVO; } }
        //=============


        public abstract bool Spawn(Coord C);

        public bool SetNewCoord(Coord C)
        {
            if (IsEmptyCell(C))
            {
                c = C;
                eventsVO.VOChangeCoord();
                return true;
            }
            return false;
        }

        protected abstract bool IsEmptyCell(Coord C);
    }



    class EventsVisualObject
    {
        public event VoidEventDelegate EventVOChangeCoord;
        public void VOChangeCoord()
        {
            EventVOChangeCoord?.Invoke();
        }
    }
}
