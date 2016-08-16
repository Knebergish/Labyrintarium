using System;
using System.Collections.Generic;

using TestOpenGL.PhisicalObjects;
using TestOpenGL.PhisicalObjects.ChieldsItem;

namespace TestOpenGL.Logic
{
    class Battle
    {
        static Random rnd = new Random();
        public static bool Attack(Being attacking, Coord C)
        {
            //Анимация атаки в ячейку C
            /*GlobalData.WorldData.Level.GetMap<Decal>().AddObject(GlobalData.OB.GetDecal(2), attacking.C);
            GlobalData.WorldData.Level.GetMap<Decal>().AddObject(GlobalData.OB.GetDecal(2), C);
            GlobalData.WorldData.Level.Pause(150);
            GlobalData.WorldData.Level.GetMap<Decal>().RemoveObject(attacking.C);
            GlobalData.WorldData.Level.GetMap<Decal>().RemoveObject(C);
            */

            if (GlobalData.WorldData.Level.GetMap<Being>().GetObject(0, C) != null)
            {
                Fight(attacking, GlobalData.WorldData.Level.GetMap<Being>().GetObject(0, C));
                return true;
            }
            return false;
        }

        public static void Fight(Being attacking, Being defending)
        {
            if (defending == null || attacking == null)
                return;


            int attack = attacking.Parameters[Feature.Power] + attacking.Parameters[Feature.Coordination] + rnd.Next(1, 11);
            int defend = defending.Parameters[Feature.Power] + defending.Parameters[Feature.Coordination] + rnd.Next(1, 11);
            int defendEvasion = defending.Parameters[Feature.Agility] + defending.Parameters[Feature.Sense] + rnd.Next(1, 11);

            GlobalData.Log.Log(attacking.GetType().Name + " " + attack.ToString() + " против " + defend.ToString());

            if (attack < defend)
                return;


            int countAttack = attacking.Parameters[Feature.Power]
                //+ attacking.Inventory.GetLevelEquipmentItemsByType<Weapon>()
                + rnd.Next(1, 5);

            int countDefend = defending.Parameters[Feature.Stamina] 
                //+ defending.Inventory.GetLevelEquipmentItemsByType<Armor>()
                + rnd.Next(1, 5);

            GlobalData.Log.Log(countAttack.ToString() + " против " + countDefend.ToString());
            if (countAttack > countDefend)
            {
                List<Weapon> lw = null;// = attacking.Inventory.GetEquipmentItemsByType<Weapon>();
                int damage = lw == null ? 0 : lw[0].Damage;

                if (damage > 0)
                    defending.Damage(damage);
                else
                    defending.Parameters.CurrentActionPoints--; //TODO: формализовать
            }


        }
    }
}
