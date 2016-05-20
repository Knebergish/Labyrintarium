using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestOpenGL;

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
                    if (Logic.Analytics.IsInArea(new Coord(4, 4), new Coord(9, 9), Program.GCycle.gamer.C))
                    {
                        Program.L.RemoveGroupDecals(0);
                        Triggers.currentTriggers.DeactivateTrigger(0);
                    }
                }));
            s1.stageLoad = delegate()
            {
                VisualObjectStructure<VisualObjects.Decal> VOSD = new VisualObjectStructure<VisualObjects.Decal>();
                for(int i = 4; i <10;i++)
                    for(int j = 4; j<10;j++)
                        VOSD.Push(Program.OB.GetDecal(3), new Coord(i,j));
                Program.L.AddDecals(VOSD);

                Program.GCycle.gamer.Spawn(new Coord(0, 0));
                Program.L.AddBeing(Program.GCycle.gamer);
            };
            s1.StartStage();
        }
    }
}
