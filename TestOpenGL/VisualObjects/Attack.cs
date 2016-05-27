﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestOpenGL;
using TestOpenGL.Logic;

namespace TestOpenGL.VisualObjects
{
    class Attack : VisualObject
    {
        public Feature profilingFeature;
        private double coefficient;
        private int minDistance, maxDistance;
        //public int length;
        private int timePause;


        public Attack(int id, string name, string description, Texture texture, Feature profilingFeature, 
            double coefficient, int minDistance, int maxDistance, int timePause)
            : base(id, name, description, texture)
        {
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
                    //TODO: херь, исправить
                    Program.L.AddDecal(new Decal(-1, "test", "test", this.texture, c));
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
            Battle.Fight(attacking,/* this,*/ Program.L.GetBeing(s.Dequeue()));
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
    }
}
