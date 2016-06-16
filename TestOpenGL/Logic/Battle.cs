using System;

using TestOpenGL.VisualObjects;
using TestOpenGL;

namespace TestOpenGL.Logic
{
    class Battle
    {
        public static void Attack(Being attacking, Coord C)
        {
            //Анимация атаки в ячейку C

            if (Program.L.MapBeings.GetBeing(C) != null)
                Fight(attacking, Program.L.MapBeings.GetBeing(C));
        }

        public static void Fight(Being attacking,Being defending)
        {
            if (defending == null || attacking == null)
                return;

            Random rnd = new Random();

            int attack = attacking.features[Feature.Power] + attacking.features[Feature.Coordination] + rnd.Next(1, 11);
            int defendWeapon = defending.features[Feature.Power] + defending.features[Feature.Coordination] + rnd.Next(1, 11);
            //int defendEvasion = defending.features[Feature.Agility] + defending.features[Feature.Sense] + rnd.Next(1, 11);

            System.Windows.Forms.MessageBox.Show(attack.ToString() + " против " + defendWeapon.ToString());

            if (attack > defendWeapon)
            {
                defending.Damage(1);
            }
            
            
            /*if (rnd.Next(1, 101) <= defending.features.Evasion)
            {
                return;
            }*/

            /*int damage = 0;// = (int)((double)attacking.features[A.profilingFeature] * A.Coefficient);
            damage += rnd.Next(-damage / 10, damage / 10 + 1);

            //damage *= (100 - defending.inventory.generalArmor) / 100;

            damage = damage <= 0 ? 1 : damage;

            defending.Damage(damage);*/
        }
    }
}
