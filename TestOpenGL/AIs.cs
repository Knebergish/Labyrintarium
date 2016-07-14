using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.Logic;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
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
                    orderby Analytics.Distance(being.C, being.C)
                    select being
                    ).ToList<Being>();

                if (Analytics.Distance(b.C, LB[0].C) > 1)
                {
                    b.Move(Analytics.DirectionToGrid(b.C, LB[0].C));
                }
                else
                {
                    b.Attack(LB[0].C);
                }
            }
            else
                b.features.ActionPoints--;
        }
    }
}
