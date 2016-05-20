namespace TestOpenGL.VisualObjects
{
    class Decal : VisualObject
    {
        public Coord C { get; set; }
        /*public Decal()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();
            C = new Coord();
        }*/
        public Decal(int id, string name, string description, Texture texture)
            : base(id, name, description, texture)
        { }

        //TODO: Может и нахрен он не нужен.
        public Decal(int id, string name, string description, Texture texture, Coord C)
            : base(id, name, description, texture)
        {
            this.C = C;
        }
    }
}
