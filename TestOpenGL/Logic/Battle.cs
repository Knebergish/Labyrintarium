using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;

namespace TestOpenGL.Logic
{
    class Battle
    {
        static Random rnd = new Random();
        public static bool Attack(Being attacking, Coord C)
        {
            //Анимация атаки в ячейку C
            Program.L.GetMap<Decal>().AddVO(Program.OB.GetDecal(2), attacking.C);
            Program.L.GetMap<Decal>().AddVO(Program.OB.GetDecal(2), C);
            Program.L.Pause(150);
            Program.L.GetMap<Decal>().RemoveVO(attacking.C);
            Program.L.GetMap<Decal>().RemoveVO(C);


            if (Program.L.GetMap<Being>().GetVO(C) != null)
            {
                Fight(attacking, Program.L.GetMap<Being>().GetVO(C));
                return true;
            }
            return false;
        }

        public static void Fight(Being attacking, Being defending)
        {
            if (defending == null || attacking == null)
                return;


            int attack = attacking.features[Feature.Power] + attacking.features[Feature.Coordination] + rnd.Next(1, 11);
            int defend = defending.features[Feature.Power] + defending.features[Feature.Coordination] + rnd.Next(1, 11);
            int defendEvasion = defending.features[Feature.Agility] + defending.features[Feature.Sense] + rnd.Next(1, 11);

            Program.Log.Log(attacking.GetType().Name + " " + attack.ToString() + " против " + defend.ToString());

            if (attack < defend)
                return;


            int countAttack = attacking.features[Feature.Power]
                + (attacking.inventory.GetEquipWeapon() != null ? attacking.inventory.GetEquipWeapon().Level : 0)
                + rnd.Next(1, 5);

            int countDefend = defending.features[Feature.Stamina] + rnd.Next(1, 5);
            List<Item> li = defending.inventory.GetEquipArmors();
            foreach (Item i in li)
                countDefend += i.Level;

            Program.Log.Log(countAttack.ToString() + " против " + countDefend.ToString());
            if (countAttack > countDefend)
                defending.Damage(1);
            
        }
    }
}
