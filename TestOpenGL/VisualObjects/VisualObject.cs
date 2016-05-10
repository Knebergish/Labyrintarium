namespace TestOpenGL.VisualObjects
{
    abstract class VisualObject
    {
        // Номер объекта в базе
        public int id { get; set; }

        public Texture texture;

        public VisualObjectInfo visualObjectInfo;

        protected VisualObject()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();
        }
    }
}
