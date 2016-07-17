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


            int attack = attacking.Features[Feature.Power] + attacking.Features[Feature.Coordination] + rnd.Next(1, 11);
            int defend = defending.Features[Feature.Power] + defending.Features[Feature.Coordination] + rnd.Next(1, 11);
            int defendEvasion = defending.Features[Feature.Agility] + defending.Features[Feature.Sense] + rnd.Next(1, 11);

            Program.Log.Log(attacking.GetType().Name + " " + attack.ToString() + " против " + defend.ToString());

            if (attack < defend)
                return;


            int countAttack = attacking.Features[Feature.Power]
                + attacking.Inventory.GetLevelEquipmentItemsByType<Weapon>()
                + rnd.Next(1, 5);

            int countDefend = defending.Features[Feature.Stamina] 
                + defending.Inventory.GetLevelEquipmentItemsByType<Armor>()
                + rnd.Next(1, 5);

            Program.Log.Log(countAttack.ToString() + " против " + countDefend.ToString());
            if (countAttack > countDefend)
                //if ((attacking.inventory.GetEquipWeapon() != null ? attacking.inventory.GetEquipWeapon().Level : 0) > 0)
                defending.Damage(1);// (attacking.inventory.GetEquipWeapon().Level);
                //else
                //    defending.features.ActionPoints--;
            
        }
    }
}
