namespace TestOpenGL
{
    abstract class GameObject : PhisicalObject, ISpawnable
    {
        GraphicsObject graphicsObject;
        //-------------


        public GameObject(GraphicsObject graphicsObject)
        {
            this.graphicsObject = graphicsObject;
        }

        public GraphicsObject GraphicsObject
        { get { return graphicsObject; } }
        //=============


        public abstract bool Spawn(int partLayer, Coord coord);

        public new bool SetNewPosition(int newPartLayer, Coord newCoord)
        {
            if (base.SetNewPosition(newPartLayer, newCoord))
            {
                graphicsObject.SetNewPosition(newPartLayer, newCoord);
                return true;
            }
            return false;
        }

        public abstract void Despawn();
    }
}
