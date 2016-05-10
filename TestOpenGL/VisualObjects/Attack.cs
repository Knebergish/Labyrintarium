using System.Collections.Generic;
using System.Threading.Tasks;
using TestOpenGL;
using TestOpenGL.Logic;

namespace TestOpenGL.VisualObjects
{
    class Attack : VisualObject
    {
        public Feature profilingFeature;
        public double coefficient;
        public int minDistance, maxDistance;
        public int length;
        public int timePause;

        public Attack()
            : base()
        {
            profilingFeature = new Feature();
        }
        public Attack(int id, Texture texture, string name, string description, Feature profilingFeature, 
            double coefficient, int minDistance, int maxDistance, int timePause) : this()
        {
            this.id = id;
            this.texture = texture;
            this.visualObjectInfo.name = name;
            this.visualObjectInfo.description = description;
            this.profilingFeature = profilingFeature;
            this.coefficient = coefficient;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            //this.length = length;
            this.timePause = timePause;
        }

        public bool UseAttack(Being attacking, Coord C)
        {
            if (Analytics.Distance(attacking.C, C) < this.minDistance
                && Analytics.Distance(attacking.C, C) > this.maxDistance)
                return false;

            //Поиск непроходимых блоков на пути атаки
            //Если такие есть, рисуем анимацию до первого из них
            //Если нету, рисуем анимацию до цели
            //Бьём цель (если есть)
            Coord c;
            Queue<Coord> s = Analytics.CoordsLine(attacking.C, C);
            
            s.Dequeue();
            while (s.Count > 1)
            {
                c = s.Dequeue();
                if (Program.L.IsPermeable(c, Passableness.Block))
                    Program.L.AddDecal(new Decal(this.texture, c));
                else break;
                //Program.GCycle.isEnabledControl = false;
                //await Task.Delay(50);//this.timePause);
                //Program.GCycle.isEnabledControl = true;
                System.Threading.Thread.Sleep(1000);
                //Program.L.Pause(1000);
                Program.L.ClearDecals();
                //this.timePause);
            }
            //Program.L.UnBlock();
            Battle.Fight(attacking, this, Program.L.GetBeing(s.Dequeue()));
            return true;
        }
    }
}
