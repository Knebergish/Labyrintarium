using System;
using System.Collections.Generic;

using TestOpenGL;
using TestOpenGL.Logic;

namespace TestOpenGL.VisualObjects
{
    abstract class Being : VisualObject
    {
        public Coord C;

        public bool isSpawned;

        public Features features;

        public Inventory inventory;

        public delegate void Action();
        public Action ActionAI;

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
            this.C = new Coord(0, 0);
            eventsBeing = new EventsBeing();
            Alliance = alliance;
            rangeOfVisibility = 10;
            //stepAI = AI.AIAttacker;
        }

        public abstract void Step();

        public void Spawn(Coord C)
        {
            if (this.isSpawned)
            {
                this.C = C;
            }
            else
            {
                this.isSpawned = true;
                this.C = C;
                if (!Program.L.MapBeings.AddBeing(this))
                    throw new Exception("Попытка добавить сущность в занятую другой сущностью ячейку.");
            }
            this.eventsBeing.BeingChangeCoord();
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
                    this.Spawn(new Coord(this.C.X + dx, this.C.Y + dy));
                    this.features.ActionPoints--;
                    return true;
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


                int temporaryIndex = Program.L.MapDecals.AddDecal(Program.OB.GetDecal(4), this.C);
                Program.L.Pause(150);
                Program.L.MapDecals.RemoveGroupDecals(temporaryIndex);
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
            this.eventsBeing.BeingDeath();
            //Program.L.RemoveBeing(this.C); //будет происходить в модуле ходения всех ботов
        }
    }

    class EventsBeing
    {
        
        public event EventDelegate EventBeingChangeCoord;
        public event EventDelegate EventBeingDeath;
        public event EventDelegate EventBeingEndActionPoints;

        public void BeingChangeCoord()
        {
            if (EventBeingChangeCoord != null)
                EventBeingChangeCoord();
        }
        public void BeingDeath()
        {
            if (EventBeingDeath != null)
                EventBeingDeath();
        }
        public void BeingEndActionPoints()
        {
            if (EventBeingEndActionPoints != null)
                EventBeingEndActionPoints();
        }
    }

}
