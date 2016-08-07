namespace TestOpenGL.PhisicalObjects
{
    class ObjectInfo
    {
        int id;
        string name;
        string description;
        //-------------


        public ObjectInfo(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public int Id
        { get { return id; } }

        public string Name
        { get { return name; } }

        public string Description
        { get { return description; } }
        //=============
    }
}
