using System.Collections.Generic;


namespace TestOpenGL
{
    class RendereableObjectsContainer
    {
        List<IRenderable> rendereableObjectsList;

        public RendereableObjectsContainer()
        {
            rendereableObjectsList = new List<IRenderable>();
        }

        public void Add(IRenderable rendereableObject)
        {
            rendereableObjectsList.Add(rendereableObject);
        }
        public void Remove(IRenderable rendereableObject)
        {
            rendereableObjectsList.Remove(rendereableObject);
        }
        public List<IRenderable> GetAllRendereableObjects()
        { return new List<IRenderable>(rendereableObjectsList); }
    }
}
