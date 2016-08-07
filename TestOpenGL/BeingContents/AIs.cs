using System.Collections.Generic;
using System.Linq;
using TestOpenGL.Logic;
using TestOpenGL.PhisicalObjects;

namespace TestOpenGL.BeingContents
{
    class AIs
    {
        public static void AIAttacker(Being b)
        {
            List<Being> LB = Analytics.GetAllEnemies(b);
            if (LB.Count != 0)
            {
                LB = (
                    from being in LB
                    orderby Analytics.Distance(b.Coord, being.Coord)
                    select being
                    ).ToList();

                if (Analytics.Distance(b.Coord, LB[0].Coord) > 1)
                {
                    //if (!b.Move(Analytics.DirectionToGrid(b.C, LB[0].C)))

                    Stack<Coord> sc = Analytics.BFS(b.Coord, LB[0].Coord);
                    if (sc.Count > 0)
                        b.Move(sc.Pop());
                    else
                        b.Parameters.CurrentActionPoints--;
                }
                else
                {
                    if(!b.Attack(LB[0].Coord))
                        b.Parameters.CurrentActionPoints--;
                }
            }
            else
                b.Parameters.CurrentActionPoints--;
        }
    }
}
