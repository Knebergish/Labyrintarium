using System;
using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    class Decal : GameObject
    {
        public Decal(Decal decal)
            : this(decal.GraphicsObject) { }
        public Decal(GraphicsObject graphicsObject)
            : base(graphicsObject) { }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (SetNewPosition(partLayer, coord))
            {
                Program.L.GetMap<Decal>().AddObject(this);
                Program.P.AddGraphicsObject(GraphicsObject);
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return Program.L.GetMap<Decal>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            Program.L.GetMap<Decal>().RemoveObject(PartLayer, Coord);
            Program.P.RemoveGraphicsObject(GraphicsObject);
        }
    }
}
