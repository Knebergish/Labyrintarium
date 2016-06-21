using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL;
using TestOpenGL.VisualObjects;

namespace TestOpenGL
{
    class Stages
    {
        
        static public void Stage_1()
        {
            Stage s1 = new Stage();
            s1.lengthX = 30;
            s1.lengthY = 30;
            s1.lengthZ = 1;
            Triggers t = new Triggers();
            //Triggers.currentTriggers = t;
            t.AddTrigger(new Trigger(0, true, delegate() 
                {
                    if (Logic.Analytics.IsInArea(new Coord(4, 4), new Coord(9, 9), Program.GCycle.Gamer.C))
                    {
                        //Program.L.MapDecals.RemoveGroupDecals(0); Устарело
                        Triggers.currentTriggers.DeactivateTrigger(0);
                    }
                }));
            s1.stageLoad = delegate()
            {
                VisualObjectStructure<VisualObjects.Decal> VOSD = new VisualObjectStructure<VisualObjects.Decal>();
                for(int i = 4; i <10;i++)
                    for(int j = 4; j<10;j++)
                        VOSD.Push(Program.OB.GetDecal(3), new Coord(i,j));
                //Program.L.MapDecals.AddDecals(VOSD); //Переделать под новый движок

                Program.GCycle.Gamer.Spawn(new Coord(0, 0));
                //Program.L.GetMap<Being>().AddVO(Program.GCycle.Gamer);
            };
            s1.StartStage();
        }
    }
}
