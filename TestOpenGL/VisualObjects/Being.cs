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

        public EventsBeing eventsBeing;
        ////////////////////////////////////////////////////////////////////////////////////////





        public Being()
        {
            texture = new Texture();
            visualObjectInfo = new VisualObjectInfo();

            ActionAI = new Action(AI);
            this.features = new Features(this);
            this.inventory = new Inventory();

            isSpawned = false;
            //TODO: сделать только параметризованный конструктор.
            this.C = new Coord(0, 0);

            eventsBeing = new EventsBeing();
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
                if (!Program.L.AddBeing(this))
                    throw new Exception("Попытка добавить сущность в занятую другой сущностью ячейку.");
            }
            this.eventsBeing.BeingChangeCoord();
        }

        public void Move(int course)
        {
            int dx = 0, dy = 0;
            switch(course)
            {
                case 0: dx--; break;
                case 1: dy++; break;
                case 2: dx++; break;
                case 3: dy--; break;
            }

            try
            {
                if (Program.L.IsPassable(new Coord(this.C.X + dx, this.C.Y + dy), true))
                    this.Spawn(new Coord(this.C.X + dx, this.C.Y + dy));
                else
                    return;

                this.features.ActionPoints--;
                return;
            }
            catch
            {
                return;
            }
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

        public void AI()
        {
            // Действия искина
            this.Spawn(Analytics.BFS(this.C, new Coord(39, 39), false).Pop());
            //this.Move(2);
        }
        public void NoAI()
        {
            // Ожидание хода игрока
            //Program.L.clickEvent.Reset();
            //Program.L.clickEvent.WaitOne();
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
