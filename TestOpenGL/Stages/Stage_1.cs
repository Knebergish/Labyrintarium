using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.BeingContents;
using TestOpenGL.Controls;
using TestOpenGL.Logic;
using TestOpenGL.Renders;
using TestOpenGL.VisualObjects;
using TestOpenGL.VisualObjects.ChieldsBeing;
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
            Program.P.StopRender();
            Program.P.ClearShadersList();
            Program.GCycle.StopStep();
            Program.L = new Level(20, 20, 4);
            VariantsControls.StandartGamerControl();
        }

        void LoadMap()
        {
            Random rnd = new Random();
            for (int x = 0; x < Program.L.LengthX; x++)
            {
                for (int y = 0; y < Program.L.LengthY; y++)
                {
                    Program.OB.GetBackground(1).Spawn(new Coord(x, y, 0));
                    //if (rnd.Next(1, 7) == 1)
                    //    Program.OB.GetBlock(2).Spawn(new Coord(x, y, 0));
                }
            }

            for (int i = 0; i < 20; i += 1)
                Program.OB.GetBlock(2).Spawn(new Coord(6, i, 0));

            new NPC(
                (Bot)Program.OB.GetBot(1, 1),
                "Тестбот",
                "Он тестовый",
                "Здравствуй, путник!",
                null
                ).Spawn(new Coord(4, 3, 3));

            Bot b = (Bot)Program.OB.GetBot(1, 3);
            b.AI = AIs.AIAttacker;
            b.Spawn(new Coord(5, 1, 0));

            Program.GCycle.Gamer = (Gamer)Program.OB.GetGamer(1);
            for (int i = 1; i < 10; i++)
                Program.GCycle.Gamer.inventory.PutBagItem(Program.OB.GetItem(i));
            Program.GCycle.Gamer.features.CurrentExperience += 100;

            Program.GCycle.Gamer.Spawn(new Coord(1, 0, 0));
            Program.GCycle.Gamer.Death();
            Program.P.Camera.Width = 30;
            Program.P.Camera.Height = 30;
        }

        void LoadShaders()
        {
            // Шейдер крон деревьев
            Program.P.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                Texture t = Program.TA.GetTexture(TypeVisualObject.Block, "3");
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Block block in Program.L.GetMap<Block>().GetAllVO())
                    if (block.Id == 2)
                        if (Analytics.CorrectCoordinate(block.C.X, block.C.Y + 1) && Analytics.IsInCamera(new Coord(block.C.X, block.C.Y + 1), Program.P.Camera))
                            lro.Add(new RenderObject(t, new Coord(block.C.X, block.C.Y + 1), (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + Program.L.LengthZ + 0.5));

                return lro;
            }));

            // Шейдер экипированных вещей
            Program.P.ShadersList.Add(new Func<List<RenderObject>>(() =>
            {
                List<RenderObject> lro = new List<RenderObject>();

                foreach (Being b in Program.L.GetMap<Being>().GetAllVO())
                    if (Analytics.IsInCamera(new Coord(b.C.X, b.C.Y), Program.P.Camera))
                        foreach (Item i in b.inventory.GetEquipmentItems())
                            lro.Add(new RenderObject(i.texture, b.C, (int)TypeVisualObject.Being * (Program.L.LengthZ - 1) + 0.1));

                return lro;
            }));
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
                        if (Program.L.GetMap<Being>().GetAllVO().Count < 5)
                        {
                            while (!GetNextBot().Spawn(new Coord(cx, 1)))
                                cx = ++cx >= 30 ? 0 : ++cx; 
                            //Program.GCycle.Gamer.features.CurrentHealth = 10;
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
            Bot b = (Bot)Program.OB.GetBot(1, currentAlliance++);
            b.AI = AIs.AIAttacker;
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