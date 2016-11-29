using System;
using System.Collections.Generic;

using TestOpenGL.BeingContents;
using TestOpenGL.Controls;
using TestOpenGL.PhisicalObjects;
using TestOpenGL.PhisicalObjects.ChieldsBeing;
using TestOpenGL.PhisicalObjects.ChieldsBlock;
using TestOpenGL.World;


namespace TestOpenGL.Stages
{
    class Stage_1
    {
        int currentAlliance = 2;
        public Stage_1()
        {

        }

        public void LoadStage()
        {
            StartLoad();
            LoadMap();
            LoadShaders();
            LoadTriggers();
            EndLoad();
        }

        void StartLoad()
        {
            GlobalData.RenderManager.StopRender();
            GlobalData.GCycle.StopStep();
            GlobalData.WorldData = new WorldData(
                new Level(20, 20, new int[5] { 4, 4, 1, 3, 1 }), 
                null, 
                null, 
                null, 
                new Renders.Camera(10, 10));
            VariantsControls.StandartGamerControl();
        }

        void LoadMap()
        {
            Random rnd = new Random();
            for (int x = 0; x < GlobalData.WorldData.Level.LengthX; x++)
            {
                for (int y = 0; y < GlobalData.WorldData.Level.LengthY; y++)
                {
                    GlobalData.OB.GetBackground(rnd.Next(1, 5)).Spawn(0, new Coord(x, y));
                    if (rnd.Next(1, 20) == 1)
                        GlobalData.OB.GetBlock(1).Spawn(0, new Coord(x, y));
                    if (rnd.Next(1, 50) == 1)
                        GlobalData.OB.GetBlock(2).Spawn(0, new Coord(x, y));
                    if (rnd.Next(1, 50) == 1)
                        GlobalData.OB.GetBlock(rnd.Next(16, 18)).Spawn(0, new Coord(x, y));
                }
            }
            List<Block> lb = GlobalData.WorldData.Level.GetMap<Block>().GetAllObject().FindAll((Block block) => { if (block.ObjectInfo.Id == 2 || block.ObjectInfo.Id == 16 || block.ObjectInfo.Id == 17) return true;  return false; });
            Block bl = lb[rnd.Next(0, lb.Count)];
            bl.Despawn();
            IBagable treeBag = new Bag(1);
            treeBag.AddItemInBag(GlobalData.OB.GetItem(15));
            new Chest
                (
                GlobalData.OB.GetBlock(bl.ObjectInfo.Id),
                treeBag,
                (() =>
                { return true; }),
                null
                ).Spawn(0, bl.Coord);

            GlobalData.GCycle.Gamer = new Gamer(GlobalData.OB.GetBeing(3));
            // До нормальной реализации стат.
            /*for (int i = 1; i < 10; i++)
                GlobalData.GCycle.Gamer.Inventory.AddItemInBag(GlobalData.OB.GetArmor(i));
            for (int i = 1; i < 5; i++)
                GlobalData.GCycle.Gamer.Inventory.AddItemInBag(GlobalData.OB.GetWeapon(i));
            GlobalData.GCycle.Gamer.Inventory.AddItemInBag(GlobalData.OB.GetShield(1));*/
            GlobalData.GCycle.Gamer.Parameters.AddExperience(100);
            GlobalData.GCycle.Gamer.Spawn(0, new Coord(5, 5));

            /*new Door
                (
                GlobalData.OB.GetBlock(15),
                GlobalData.OB.GetGraphicObject(12, Layer.Block),
                true,
                false
                ).Spawn(0, new Coord(5, 4));*/

            //Bot b = new Bot(GlobalData.OB.GetBeing(3), AIs.AIAttacker);
            //b.Spawn(0, new Coord(0, 0));

            NPC npc = new NPC
                (
                GlobalData.OB.GetBeing(2),
                "Здравствуй, путник!",
                null
                );

            npc.Spawn(0, new Coord(4, 4));
            npc.Inventory.EquipFromWithout(GlobalData.OB.GetItem(12));


            IBagable bag = new Bag(20);
            bag.AddItemInBag(GlobalData.OB.GetItem(1));
            bag.AddItemInBag(GlobalData.OB.GetItem(5));

            new Chest
                (
                GlobalData.OB.GetBlock(14),
                bag,
                (() =>
                {
                    if (GlobalData.GCycle.Gamer.Inventory.GetAllBagItems()?.Find((Item item) => { if (item.ObjectInfo.Name == "Ключ") return true; return false; }) != null)
                        return true;
                    return false;
                }),
                "Вы не нашли подходящего ключа."
                ).Spawn(1, new Coord(3, 7));
        }

        void LoadShaders()
        {
            // Шейдер крон деревьев
            /*GlobalData.RenderManager.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                Texture t = Program.TA.GetTexture(TypeVisualObject.Block, "3");
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Block block in GlobalData.WorldData.Level.GetMap<Block>().GetAllVO())
                    if (block.ObjectInfo.Id == 2)
                        if (Analytics.CorrectCoordinate(block.C.X, block.C.Y + 1) && Analytics.IsInCamera(new Coord(block.C.X, block.C.Y + 1), GlobalData.WorldData.Camera))
                            lro.Add(new RenderObject(t, new Coord(block.C.X, block.C.Y + 1), (int)TypeVisualObject.Being * (GlobalData.WorldData.Level.LengthZ - 1) + GlobalData.WorldData.Level.LengthZ + 0.5));

                return lro;
            }));*/

            // Шейдер экипированных вещей
            /*GlobalData.RenderManager.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Being b in GlobalData.WorldData.Level.GetMap<Being>().GetAllVO())
                    if (Analytics.IsInCamera(new Coord(b.C.X, b.C.Y), GlobalData.WorldData.Camera))
                    {
                        List<Armor> la = b.Inventory.GetEquipmentItemsByType<Armor>() ?? new List<Armor>();
                        foreach (Item i in la)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (GlobalData.WorldData.Level.LengthZ - 1) + 0.1));

                        List<Weapon> lw = b.Inventory.GetEquipmentItemsByType<Weapon>() ?? new List<Weapon>();
                        foreach (Item i in lw)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (GlobalData.WorldData.Level.LengthZ - 1) + 0.1));

                        List<Shield> ls = b.Inventory.GetEquipmentItemsByType<Shield>() ?? new List<Shield>();
                        foreach (Item i in ls)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (GlobalData.WorldData.Level.LengthZ - 1) + 0.1));
                    }
                        
                return lro;
            }));*/
        }

        void LoadTriggers()
        {
            /*Triggers.currentTriggers.AddTrigger(
                new Trigger(
                    1,
                    true,
                    delegate 
                    {
                        int cx = 5;
                        if (GlobalData.WorldData.Level.GetMap<Being>().GetAllObject().Count < 5)
                        {
                            while (!GetNextBot().Spawn(0, new Coord(cx, 1)))
                                cx = ++cx >= 30 ? 0 : ++cx; 
                            GlobalData.GCycle.Gamer.Parameters.CurrentHealth = 10;
                        }
                    }
                ));*/
            /*Triggers.currentTriggers.AddTrigger(
                new Trigger(
                    1, 
                    true,
                    delegate
                    {
                        if (GlobalData.WorldData.Level.GetMap<Being>().GetObject(0, new Coord(0, 1)) != null)
                        {
                            Block b = GlobalData.OB.GetBlock(15);
                            b.Spawn(1, new Coord(5, 6));
                        }
                    }
                    )
                );*/
        }

        void EndLoad()
        {
            GlobalData.Log.SetCurrentQuest("Найди ключ от сундука в дереве!");
            GlobalData.FA.UpdateForms();
            GlobalData.RenderManager.StartRender();
            GlobalData.GCycle.StartStep();
        }

        Bot GetNextBot()
        {
            Bot b = new Bot(GlobalData.OB.GetBeing(1), AIs.AIAttacker);
            b.Alliance = currentAlliance++;
            return b;
        }
    }
}