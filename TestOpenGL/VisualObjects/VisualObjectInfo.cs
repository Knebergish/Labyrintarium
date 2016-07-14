namespace TestOpenGL.VisualObjects
{
    class VisualObjectInfo
    {
        string name;
        string description;
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        //-------------


        public VisualObjectInfo(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
        //=============
    }
}
