using System;
using System.Collections.Generic;

using TestOpenGL;
using TestOpenGL.Logic;

namespace TestOpenGL.VisualObjects
{
    abstract class Being : VisualObject
    {
        //public Coord C;

        public bool isSpawned;

        public Features features;

        public Inventory inventory;

        //Func<List<object>, bool> stepAI;

        public EventsBeing eventsBeing;

        private int rangeOfVisibility;

        public int RangeOfVisibility
        {
            get { return rangeOfVisibility; }
            set 
            { 
                rangeOfVisibility = value; 
            }
        }

        public int Alliance
        { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////

        public Being(int id, string name, string description, Texture texture, int alliance)
            : base(id, name, description, texture)
        {
            this.features = new Features(this);
            this.inventory = new Inventory();
            isSpawned = false;
            eventsBeing = new EventsBeing();
            Alliance = alliance;
            rangeOfVisibility = 10;
        }

        protected override bool IsEmptyCell(Coord C)
        {
            return Program.L.GetMap<Being>().GetVO(C) == null ? true : false;
        }

        public void Step()
        {
            this.eventsBeing.BeingStartStep();
            //this.features.ActionPoints += this.features.IncreaseActionPoints;
            while (!CheckEndStep())
            {
                Action();
                //System.Threading.Thread.Sleep(100);
                this.eventsBeing.BeingEndAction();
            }
            this.eventsBeing.BeingEndStep();
        }
        public abstract void Action();

        public bool CheckEndStep()
        {
            return this.features.ActionPoints < 1 ? true : false;
        }

        public override bool Spawn(Coord C)
        {
            if(SetNewCoord(C))
            {
                Program.L.GetMap<Being>().AddVO(this, C);
                isSpawned = true;
                return true;
            }
            return false;
        }

        public bool Move(Coord C)
        {
            if(this.features.ActionPoints >= 1 && SetNewCoord(C))
            {
                Program.L.Pause(100);
                this.features.ActionPoints--;
                return true;
            }
            return false;
        }
        public bool Move(TestOpenGL.Direction course)
        {
            int dx = 0, dy = 0;
            switch(course)
            {
                case Direction.Left: dx--; break;
                case Direction.Up: dy++; break;
                case Direction.Right: dx++; break;
                case Direction.Down: dy--; break;
            }

            if (Analytics.CorrectCoordinate(this.C.X + dx, this.C.Y + dy))
                if (Program.L.IsPassable(new Coord(this.C.X + dx, this.C.Y + dy)))
                {
                    return Move(new Coord(this.C.X + dx, this.C.Y + dy));
                }
            return false;
        }

        public void Damage(int count)
        {
            if (count > 0)
            {
                this.features.CurrentHealth -= count;

                // Переделать под новый движок
                //int temporaryIndex = Program.L.MapDecals.AddDecal(Program.OB.GetDecal(4), this.C);
                Program.L.Pause(150);
                //Program.L.MapDecals.RemoveGroupDecals(temporaryIndex);
            }
            else throw new Exception("Урон почему-то отрицательный.");
        }

        public void Heal(int count)
        {
            if (count > 0)
                this.features.CurrentHealth += count;
            else throw new Exception("Исцеление почему-то отрицательное.");
        }

        public void Death()
        {
            this.isSpawned = false;
            this.eventsBeing.BeingDeath();
            Program.L.GetMap<Being>().RemoveVO(this.C);
        }
    }

    class EventsBeing
    {
        public event VoidEventDelegate EventBeingDeath;
        public event VoidEventDelegate EventBeingStartStep;
        public event VoidEventDelegate EventBeingEndStep;
        public event VoidEventDelegate EventBeingEndAction;
        public event VoidEventDelegate EventBeingEndActionPoint;

        public void BeingDeath()
        {
            if (EventBeingDeath != null)
                EventBeingDeath();
        }
        public void BeingStartStep()
        {
            if (EventBeingStartStep != null)
                EventBeingStartStep();
        }
        public void BeingEndStep()
        {
            if (EventBeingEndStep != null)
                EventBeingEndStep();
        }
        public void BeingEndAction()
        {
            if (EventBeingEndAction != null)
                EventBeingEndAction();
        }
        public void BeingEndActionPoint()
        {
            if (EventBeingEndActionPoint != null)
                EventBeingEndActionPoint();
        }
    }
}
