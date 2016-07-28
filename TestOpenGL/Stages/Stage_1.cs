﻿using System;
using TestOpenGL.BeingContents;
using TestOpenGL.Controls;
using TestOpenGL.VisualObjects;
using TestOpenGL.VisualObjects.ChieldsBeing;
using TestOpenGL.VisualObjects.ChieldsBlock;
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
            //LoadShaders();
            //LoadTriggers();
            EndLoad();
        }

        void StartLoad()
        {
            Program.P.StopRender();
            Program.P.ClearShadersList();
            Program.GCycle.StopStep();
            Program.L = new Level(20, 20, new int[5] { 4, 4, 1, 3, 1 });
            VariantsControls.StandartGamerControl();
        }

        void LoadMap()
        {
            Random rnd = new Random();
            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY; y++)
                {
                    Program.OB.GetBackground(1).Spawn(0, new Coord(x, y));
                    //if (rnd.Next(1, 7) == 1)
                    //    Program.OB.GetBlock(2).Spawn(new Coord(x, y, 0));
                }
            }

            for (int i = 0; i < 20; i += 3)
                Program.OB.GetBlock(2).Spawn(2, new Coord(6, i));

            //Bot b = new Bot(Program.OB.GetBeing(1), AIs.AIAttacker);
            //b.Spawn(0, new Coord(5, 1));


            Program.GCycle.Gamer = new Gamer(Program.OB.GetBeing(2));
            for (int i = 1; i < 10; i++)
                Program.GCycle.Gamer.Inventory.AddItemInBag(Program.OB.GetArmor(i));
            for (int i = 1; i < 5; i++)
                Program.GCycle.Gamer.Inventory.AddItemInBag(Program.OB.GetWeapon(i));
            Program.GCycle.Gamer.Inventory.AddItemInBag(Program.OB.GetShield(1));
            Program.GCycle.Gamer.Features.CurrentExperience += 100;
            Program.GCycle.Gamer.Spawn(0, new Coord(0, 0));

            new NPC(
                Program.OB.GetBeing(3),
                "Здравствуй, путник!",
                null
                ).Spawn(0, new Coord(4, 3));

            new Door(
                Program.OB.GetBlock(15), 
                Program.OB.GetGraphicObject(12, Layer.Block), 
                true, 
                false
                ).Spawn(0, new Coord(1, 4));

            IBagable bag = new Bag(20);
            bag.AddItemInBag(Program.OB.GetItem(1));
            bag.AddItemInBag(Program.OB.GetItem(5));

            new Chest
            (
            Program.OB.GetBlock(14),
            bag
            ).Spawn(1, new Coord(3, 3));

            Program.P.Camera.Width = 30;
            Program.P.Camera.Height = 30;
        }

        void LoadShaders()
        {
            // Шейдер крон деревьев
            /*Program.P.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                Texture t = Program.TA.GetTexture(TypeVisualObject.Block, "3");
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Block block in Program.L.GetMap<Block>().GetAllVO())
                    if (block.ObjectInfo.Id == 2)
                        if (Analytics.CorrectCoordinate(block.C.X, block.C.Y + 1) && Analytics.IsInCamera(new Coord(block.C.X, block.C.Y + 1), Program.P.Camera))
                            lro.Add(new RenderObject(t, new Coord(block.C.X, block.C.Y + 1), (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + Program.L.LengthZ + 0.5));

                return lro;
            }));*/

            // Шейдер экипированных вещей
            /*Program.P.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Being b in Program.L.GetMap<Being>().GetAllVO())
                    if (Analytics.IsInCamera(new Coord(b.C.X, b.C.Y), Program.P.Camera))
                    {
                        List<Armor> la = b.Inventory.GetEquipmentItemsByType<Armor>() ?? new List<Armor>();
                        foreach (Item i in la)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + 0.1));

                        List<Weapon> lw = b.Inventory.GetEquipmentItemsByType<Weapon>() ?? new List<Weapon>();
                        foreach (Item i in lw)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + 0.1));

                        List<Shield> ls = b.Inventory.GetEquipmentItemsByType<Shield>() ?? new List<Shield>();
                        foreach (Item i in ls)
                            lro.Add(new RenderObject(i.Texture, b.C, (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + 0.1));
                    }
                        
                return lro;
            }));*/
        }

        void LoadTriggers()
        {
            Triggers.currentTriggers.AddTrigger(
                new Trigger(
                    1,
                    true,
                    delegate 
                    {
                        int cx = 5;
                        if (Program.L.GetMap<Being>().GetAllObject().Count < 5)
                        {
                            while (!GetNextBot().Spawn(0, new Coord(cx, 1)))
                                cx = ++cx >= 30 ? 0 : ++cx; 
                            Program.GCycle.Gamer.Features.CurrentHealth = 10;
                        }
                    }
                ));
        }

        void EndLoad()
        {
            Program.Log.SetCurrentQuest("Не умри!!");
            Program.P.StartRender();
            Program.GCycle.StartStep();
        }

        Bot GetNextBot()
        {
            Bot b = new Bot(Program.OB.GetBeing(1), AIs.AIAttacker);
            b.Alliance = currentAlliance++;
            return b;
        }
    }
}

/*static public void Stage_1()
{
    Stage s1 = new Stage();
    s1.lengthX = 30;
    s1.lengthY = 30;
    s1.lengthZ = 1;
    Triggers t = new Triggers();
    //Triggers.currentTriggers = t;
    t.AddTrigger(new Trigger(0, true, delegate ()
    {
        if (Logic.Analytics.IsInArea(new Coord(4, 4), new Coord(9, 9), Program.GCycle.Gamer.C))
        {
            //Program.L.MapDecals.RemoveGroupDecals(0); Устарело
            Triggers.currentTriggers.DeactivateTrigger(0);
        }
    }));
    s1.stageLoad = delegate ()
    {
        //VisualObjectStructure<VisualObjects.Decal> VOSD = new VisualObjectStructure<VisualObjects.Decal>();
        //for(int i = 4; i <10;i++)
        //    for(int j = 4; j<10;j++)
        //        VOSD.Push(Program.OB.GetDecal(3), new Coord(i,j));
        //Program.L.MapDecals.AddDecals(VOSD); //Переделать под новый движок

        Program.GCycle.Gamer.Spawn(new Coord(0, 0));
        //Program.L.GetMap<Being>().AddVO(Program.GCycle.Gamer);
    };
    s1.StartStage();
}*/