using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    abstract class VisualObject
    {
        // Номер объекта в базе
        int id;
        
        Coord c;

        public EventsVisualObject eventsVO;
        
        protected TypeVisualObject tvo;

        public VisualObjectInfo visualObjectInfo;

        public Texture texture;
        //-------------


        protected VisualObject(int id, string name, string description, Texture texture)
        {
            this.id = id;
            visualObjectInfo = new VisualObjectInfo(name, description);
            this.texture = texture;
            eventsVO = new EventsVisualObject();
        }

        public int Id { get { return id; } }

        public Coord C
        {
            get { return c; }
        }
        //=============


        public abstract bool Spawn(Coord C);

        public bool SetNewCoord(Coord C)
        {
            if (this.IsEmptyCell(C))
            {
                this.c = C;
                this.eventsVO.VOChangeCoord();
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
            if (EventVOChangeCoord != null)
                EventVOChangeCoord();
        }
    }
}
