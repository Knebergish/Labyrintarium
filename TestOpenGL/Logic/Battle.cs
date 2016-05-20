using System;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL.Logic
{
    class Battle
    {
        public static void Fight(Being attacking, Attack A, Being defending)
        {
            if (defending == null)
                return;

            Random rnd = new Random();

            if (rnd.Next(1, 101) <= defending.features.Evasion)
            {
                return;
            }

            int damage = (int)((double)attacking.features[A.profilingFeature] * A.Coefficient);
            damage += rnd.Next(-damage / 10, damage / 10 + 1);

            //damage *= (100 - defending.inventory.generalArmor) / 100;

            damage = damage <= 0 ? 1 : damage;

            defending.Damage(damage);
        }
    }
}
