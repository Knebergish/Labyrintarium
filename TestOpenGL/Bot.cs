using TestOpenGL.VisualObjects;
using TestOpenGL.Logic;
using TestOpenGL;

namespace TestOpenGL
{
    class Bot: Being
    {
        bool isEndStep = false;

        public Bot(int id, string name, string description, Texture texture)
            : base(id, name, description, texture)
        {
            this.eventsBeing.EventBeingEndActionPoints += new EventDelegate(EndStep);
        }

        public override void Step()
        {
            while (!isEndStep)
            {
                try
                {
                    this.Spawn(Analytics.BFS(this.C, new Coord(Program.GCycle.sight.AimCoord.X, Program.GCycle.sight.AimCoord.Y)/*, false*/).Pop());
                    this.features.ActionPoints--;
                }
                catch
                {
                    //TODO: Действия, если нет пути.
                }
                
            }
            isEndStep = false;
        }
        private void EndStep()
        {
            isEndStep = true;
        }
    }
}
