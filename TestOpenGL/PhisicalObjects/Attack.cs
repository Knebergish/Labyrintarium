using System.Collections.Generic;
using System.Threading.Tasks;
using TestOpenGL;
using TestOpenGL.Logic;

namespace TestOpenGL.PhisicalObjects
{
    /*class Attack : VisualObject
    {
        public Feature profilingFeature;
        private double coefficient;
        private int minDistance, maxDistance;
        private int timePause;


        public Attack(int id, string name, string description, Texture texture, Feature profilingFeature, 
            double coefficient, int minDistance, int maxDistance, int timePause)
            : base(id, name, description, texture)
        {
            this.profilingFeature = profilingFeature;
            this.coefficient = coefficient;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.timePause = timePause;
        }

        static public void AttackAnimation(Coord start, Coord end, Decal decal, int pause)
        {
            Coord c;
            int temp;
            Queue<Coord> s = Analytics.CoordsLine(start, end);

            s.Dequeue();
            while (s.Count > 1)
            {
                c = s.Dequeue();
                if (GlobalData.WorldData.Level.IsPermeable(c, Permeability.Block))
                    temp = GlobalData.WorldData.Level.MapDecals.AddDecal(decal, c);
                else break;

                System.Threading.Thread.Sleep(pause);

                GlobalData.WorldData.Level.MapDecals.RemoveGroupDecals(temp);
            }
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
                if (GlobalData.WorldData.Level.IsPermeable(c, Permeability.Block))
                    //TODO: херь, исправить
                    GlobalData.WorldData.Level.MapDecals.AddDecal(new Decal(-1, "test", "test", this.texture, c));
                else break;
                //GlobalData.GCycle.isEnabledControl = false;
                //await Task.Delay(50);//this.timePause);
                //GlobalData.GCycle.isEnabledControl = true;
                System.Threading.Thread.Sleep(1000);
                //GlobalData.WorldData.Level.Pause(1000);
                GlobalData.WorldData.Level.MapDecals.ClearDecals();
                //this.timePause);
            }
            //GlobalData.WorldData.Level.UnBlock();
            //Battle.Fight(attacking, this, 
    GlobalData.WorldData.Level.MapBeings.GetBeing(s.Dequeue()));
            return true;
        }

        public double Coefficient
        {
            get { return coefficient; }
        }
        public int MaxDistance
        {
            get { return maxDistance; }
        }
        public int MinDistance
        {
            get { return minDistance; }
        }
        public int TimePause
        {
            get { return timePause; }
        }

        public override bool Spawn(Coord C)
        {
            if (SetNewCoord(C))
            {
                GlobalData.WorldData.Level.GetMap<Block>().AddVO(this, C);
                return true;
            }
            return false;
        }
        protected override bool IsEmptyCell(Coord C)
        {
            return GlobalData.WorldData.Level.GetMap<Block>().GetVO(C) == null ? true : false;
        }
    }*/
}
