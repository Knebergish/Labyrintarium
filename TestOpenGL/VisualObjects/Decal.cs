namespace TestOpenGL.VisualObjects
{
    class Decal : VisualObject
    {
        public Coord C;
        public Decal()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();
            C = new Coord();
        }
        public Decal(Texture texture)
        {
            this.texture = texture;
            visualObjectInfo = new VisualObjectInfo();
            C = new Coord();
        }
        public Decal(Texture texture, Coord C)
        {
            this.texture = texture;
            visualObjectInfo = new VisualObjectInfo();
            this.C = C;
        }
    }
}
