using System.Collections.Generic;
using System.Linq;
using TestOpenGL.Logic;
using TestOpenGL.VisualObjects;

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
                    orderby Analytics.Distance(b.C, being.C)
                    select being
                    ).ToList();

                if (Analytics.Distance(b.C, LB[0].C) > 1)
                {
                    //if (!b.Move(Analytics.DirectionToGrid(b.C, LB[0].C)))

                    Stack<Coord> sc = Analytics.BFS(b.C, LB[0].C);
                    if (sc.Count > 0)
                        b.Move(sc.Pop());
                    else
                        b.Features.ActionPoints--;
                }
                else
                {
                    if(!b.Attack(LB[0].C))
                        b.Features.ActionPoints--;
                }
            }
            else
                b.Features.ActionPoints--;
        }
    }
}
