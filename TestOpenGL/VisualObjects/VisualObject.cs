namespace TestOpenGL.VisualObjects
{
    abstract class VisualObject
    {
        // Номер объекта в базе
        int id;
        public int Id { get { return id; } }

        public VisualObjectInfo visualObjectInfo;

        public Texture texture;

        
        protected VisualObject(int id, string name, string description, Texture texture)
        {
            this.id = id;
            visualObjectInfo = new VisualObjectInfo(name, description);
            this.texture = texture;
        }
    }
}
