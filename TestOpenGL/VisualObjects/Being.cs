using System;

using TestOpenGL.BeingContents;
using TestOpenGL.Logic;
using TestOpenGL.Renders;

namespace TestOpenGL.VisualObjects
{
    abstract class Being : VisualObject
    {
        private bool isSpawned;

        public Features features;

        public Inventory inventory;

        public EventsBeing eventsBeing;

        private int rangeOfVisibility;
        //-------------


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

        public bool IsSpawned
        {
            get { return isSpawned; }
            //set { isSpawned = value; }
        }
        //=============


        public override bool Spawn(Coord C)
        {
            if (!isSpawned && SetNewCoord(new Coord(C.X, C.Y)))
            {
                Program.L.GetMap<Being>().AddVO(this, C);
                Program.GCycle.EventStepBeings += Step;
                Program.GCycle.EventStepBeingsIncrease += Increace;
                isSpawned = true;
                return true;
            }
            return false;
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

        protected abstract void Action();

        bool CheckEndStep()
        {
            return this.features.ActionPoints < 1 ? true : false;
        }
        

        public bool Move(Coord C)
        {
            if(isSpawned && this.features.ActionPoints >= 1 && SetNewCoord(C))
            {
                Program.L.Pause(100);
                features.ActionPoints--;
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

        /// <summary>
        /// Нанесение урона сущности.
        /// </summary>
        /// <param name="count"> Величина наносимого урона. Должна быть > 0.</param>
        public void Damage(int count)
        {
            if (count > 0)
            {
                this.features.CurrentHealth -= count;

                Program.L.GetMap<Decal>().AddVO(Program.OB.GetDecal(4), C);
                Program.L.Pause(150);
                Program.L.GetMap<Decal>().RemoveVO(C);
            }
            else throw new Exception("Урон почему-то отрицательный.");
        }

        /// <summary>
        /// Исцеление сущности.
        /// </summary>
        /// <param name="count"> Величина исцеления. Должна быть > 0.</param>
        public void Heal(int count)
        {
            if (count > 0)
                this.features.CurrentHealth += count;
            else throw new Exception("Исцеление почему-то отрицательное.");
        }

        public void Death()
        {
            this.isSpawned = false;
            //TODO: проверить работоспособность этого кода
            Program.GCycle.EventStepBeings -= Step;
            Program.GCycle.EventStepBeingsIncrease -= Increace;
            //
            this.eventsBeing.BeingDeath();
            Program.L.GetMap<Being>().RemoveVO(this.C);
        }

        public bool Use()
        {
            if (Analytics.Distance(this.C, Program.P.Camera.Sight.C) > 1)
                return false;

            foreach (Block b in Program.L.GetMap<Block>().GetCellVO(Program.P.Camera.Sight.C))
                if (b is IUsable)
                    ((IUsable)b).Used();

            foreach (Being b in Program.L.GetMap<Being>().GetCellVO(Program.P.Camera.Sight.C))
                if (b is IUsable)
                    ((IUsable)b).Used();
            return true;
        }

        public bool Attack(Coord C)
        {
            if(Battle.Attack(this, C))
            {
                features.ActionPoints--;
                return true;
            }
            return false;
        }

        public void Increace()
        {
            features.ActionPoints += features.IncreaseActionPoints;
            //TODO: восстановление жизней тут же
        }

        protected override bool IsEmptyCell(Coord C)
        {
            //if(Program.L.GetMap<Being>().GetCellVO(C)!=null)
            return Program.L.GetMap<Being>().GetCellVO(C).Count == 0 ? true : false;
        }
    }

    class EventsBeing
    {
        public event VoidEventDelegate EventBeingDeath;
        public event VoidEventDelegate EventBeingStartStep;
        public event VoidEventDelegate EventBeingEndStep;
        public event VoidEventDelegate EventBeingEndAction;
        public event VoidEventDelegate EventBeingEndActionPoints;
        //-------------


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
        public void BeingEndActionPoints()
        {
            if (EventBeingEndActionPoints != null)
                EventBeingEndActionPoints();
        }
    }

}
