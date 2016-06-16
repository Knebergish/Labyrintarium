using System.Collections.Generic;
using System.Linq;

using TestOpenGL.VisualObjects;
using TestOpenGL.Logic;
using TestOpenGL;

namespace TestOpenGL
{
    class Bot: Being
    {
        bool isEndStep = false;

        public Bot(int id, string name, string description, Texture texture, int alliance)
            : base(id, name, description, texture, alliance)
        {
            this.eventsBeing.EventBeingEndActionPoints += new EventDelegate(EndStep);
        }

        public override void Step()
        {
            while (!isEndStep)
            {
                /*try
                {
                    //this.Spawn(Analytics.BFS(this.C, new Coord(Program.GCycle.sight.AimCoord.X, Program.GCycle.sight.AimCoord.Y)).Pop());
                    Coord C = Analytics.BFS(this.C, new Coord(29, 29)).Pop();
                    if (Program.L.IsPassable(C))
                    {
                        this.Spawn(C);
                        System.Threading.Thread.Sleep(20);
                    }
                    //if(!Move(TestOpenGL.Direction.Up))
                    //    Move(TestOpenGL.Direction.Right);
                    //Move(Analytics.DirectionToGrid(this.C, new Coord(29, 29)));
                    this.features.ActionPoints--;
                }
                catch
                {
                    //TODO: Действия, если нет пути.
                    this.features.ActionPoints--;
                }**/

                List<Being> LB = Analytics.GetAllEnemies(this);
                if (LB.Count != 0)
                {
                    LB = (
                        from b in LB
                        orderby Analytics.Distance(this.C, b.C)
                        select b
                        ).ToList<Being>();

                    if(Analytics.Distance(this.C, LB[0].C) > 1)
                    {
                        Move(Analytics.DirectionToGrid(this.C, LB[0].C));
                    }
                    else
                    {
                        LB[0].Damage(1);
                    }
                }
                this.features.ActionPoints--;
            }
            isEndStep = false;
        }
        private void EndStep()
        {
            isEndStep = true;
        }
    }
}
