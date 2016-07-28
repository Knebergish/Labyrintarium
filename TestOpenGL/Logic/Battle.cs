using System;
using System.Collections.Generic;

using TestOpenGL.VisualObjects;
using TestOpenGL.VisualObjects.ChieldsItem;

namespace TestOpenGL.Logic
{
    class Battle
    {
        static Random rnd = new Random();
        public static bool Attack(Being attacking, Coord C)
        {
            //Анимация атаки в ячейку C
            /*Program.L.GetMap<Decal>().AddObject(Program.OB.GetDecal(2), attacking.C);
            Program.L.GetMap<Decal>().AddObject(Program.OB.GetDecal(2), C);
            Program.L.Pause(150);
            Program.L.GetMap<Decal>().RemoveObject(attacking.C);
            Program.L.GetMap<Decal>().RemoveObject(C);
            */

            if (Program.L.GetMap<Being>().GetObject(0, C) != null)
            {
                Fight(attacking, Program.L.GetMap<Being>().GetObject(0, C));
                return true;
            }
            return false;
        }

        public static void Fight(Being attacking, Being defending)
        {
            if (defending == null || attacking == null)
                return;


            int attack = attacking.Features[Feature.Power] + attacking.Features[Feature.Coordination] + rnd.Next(1, 11);
            int defend = defending.Features[Feature.Power] + defending.Features[Feature.Coordination] + rnd.Next(1, 11);
            int defendEvasion = defending.Features[Feature.Agility] + defending.Features[Feature.Sense] + rnd.Next(1, 11);

            Program.Log.Log(attacking.GetType().Name + " " + attack.ToString() + " против " + defend.ToString());

            if (attack < defend)
                return;


            int countAttack = attacking.Features[Feature.Power]
                //+ attacking.Inventory.GetLevelEquipmentItemsByType<Weapon>()
                + rnd.Next(1, 5);

            int countDefend = defending.Features[Feature.Stamina] 
                //+ defending.Inventory.GetLevelEquipmentItemsByType<Armor>()
                + rnd.Next(1, 5);

            Program.Log.Log(countAttack.ToString() + " против " + countDefend.ToString());
            if (countAttack > countDefend)
            {
                List<Weapon> lw = null;// = attacking.Inventory.GetEquipmentItemsByType<Weapon>();
                int damage = lw == null ? 0 : lw[0].Damage;

                if (damage > 0)
                    defending.Damage(damage);
                else
                    defending.Features.ActionPoints--; //TODO: формализовать
            }


        }
    }
}
