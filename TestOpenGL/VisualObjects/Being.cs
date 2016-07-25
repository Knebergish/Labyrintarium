using System;
using System.Collections.Generic;

using TestOpenGL.BeingContents;
using TestOpenGL.Logic;
using TestOpenGL.Renders;
using TestOpenGL.VisualObjects.ChieldsItem;

namespace TestOpenGL.VisualObjects
{
    class Being : PhisicalObject
    {
        bool isSpawned;
        int rangeOfVisibility;

        Features features;
        Inventory inventory;

        EventsBeing eventsBeing;
        //-------------


        public Being(Being being)
            : this(being.GraphicObjectsPack, being.ObjectInfo, being.Alliance) { }

        public Being(GraphicObjectsPack graphicObjectPack, ObjectInfo objectInfo, int alliance)
            : this(graphicObjectPack, objectInfo, alliance, null, null) { }

        public Being(GraphicObjectsPack graphicObjectPack, ObjectInfo objectInfo, int alliance, Features features, Inventory inventory)
            : base(graphicObjectPack, objectInfo)
        {
            NewPositionCheck += Program.L.IsPassable;

            isSpawned = false;
            rangeOfVisibility = 10;

            this.features =  features ?? new Features(this);
            this.inventory =  inventory ?? new Inventory();
            this.inventory.Owner = this;

            eventsBeing = new EventsBeing();

            Alliance = alliance;
        }

        public int RangeOfVisibility
        {
            get { return rangeOfVisibility; }
            set { rangeOfVisibility = value; }
        }

        public int Alliance
        { get; set; }

        public bool IsSpawned
        {
            get { return isSpawned; }
        }

        public Features Features
        { get { return features; } }

        public Inventory Inventory
        { get { return inventory; } }

        public  EventsBeing EventsBeing
        { get { return eventsBeing; } }
        //=============


        public override bool Spawn(int partLayer, Coord coord)
        {
            if (partLayer != 0)
                return false;

            if (!isSpawned && SetNewPosition(0, coord))
            {
                Program.L.GetMap<Being>().AddObject(this);
                Program.P.AddRenderObject(GraphicObjectsPack);
                Program.GCycle.EventStepBeings += Step;
                Program.GCycle.EventStepBeingsIncrease += Increace;
                isSpawned = true;
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return Program.L.GetMap<Being>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            isSpawned = false;
            //TODO: проверить работоспособность этого кода
            Program.GCycle.EventStepBeings -= Step;
            Program.GCycle.EventStepBeingsIncrease -= Increace;
            //
            Program.L.GetMap<Being>().RemoveObject(PartLayer, Coord);
            Program.P.RemoveRenderObject(GraphicObjectsPack);
            eventsBeing.BeingDeath();
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

        protected virtual void Action() { }

        bool CheckEndStep()
        {
            return this.features.ActionPoints < 1 ? true : false;
        }
        
        public bool Move(Coord coord)
        {
            if(isSpawned && this.features.ActionPoints >= 1 && SetNewPosition(0, coord))
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

            if (Analytics.CorrectCoordinate(Coord.X + dx, Coord.Y + dy))
                if (Program.L.IsPassable(PartLayer, new Coord(Coord.X + dx, Coord.Y + dy)))
                {
                    return Move(new Coord(Coord.X + dx, Coord.Y + dy));
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
                features.CurrentHealth -= count;
                Action removeDecal = Program.DA.AddDecal(Program.OB.GetDecal(3), Coord);
                Program.L.Pause(150);
                removeDecal();
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
                features.CurrentHealth += count;
            else throw new Exception("Исцеление почему-то отрицательное.");
        }

        public bool Use()
        {
            if (Analytics.Distance(Coord, Program.P.Camera.Sight.Coord) > 1)
                return false;

            foreach (Block b in Program.L.GetMap<Block>().GetCellObject(Program.P.Camera.Sight.Coord))
                if (b is IUsable)
                    ((IUsable)b).Used();

            foreach (Being b in Program.L.GetMap<Being>().GetCellObject(Program.P.Camera.Sight.Coord))
                if (b is IUsable)
                    ((IUsable)b).Used();
            return true;
        }

        //TODO: говнокод
        public bool Attack(Coord coord)
        {
            List<Weapon> lw = Inventory.GetEquipmentItemsByType<Weapon>() ?? new List<Weapon>(); //...
            if (lw.Count != 0)
                if (
                    lw[0].MinDistance <= Analytics.Distance(Coord, coord)
                    && lw[0].MaxDistance >= Analytics.Distance(Coord, coord)
                    )
                    if (Battle.Attack(this, coord))
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
    }

    class EventsBeing
    {
        public event VoidEventDelegate EventBeingDeath;
        public event VoidEventDelegate EventBeingStartStep;
        public event VoidEventDelegate EventBeingEndStep;
        public event VoidEventDelegate EventBeingEndAction;
        public event VoidEventDelegate EventBeingEndActionPoints;
        public event VoidEventDelegate EventBeingChangeActionPoints;
        public event VoidEventDelegate EventBeingChangeHealth;
        //-------------


        public void BeingDeath()
        {
            EventBeingDeath?.Invoke();
        }
        public void BeingStartStep()
        {
            EventBeingStartStep?.Invoke();
        }
        public void BeingEndStep()
        {
                EventBeingEndStep?.Invoke();
        }
        public void BeingEndAction()
        {
                EventBeingEndAction?.Invoke();
        }
        public void BeingEndActionPoints()
        {
                EventBeingEndActionPoints?.Invoke();
        }
        public void BeingChangeActionPoints()
        {
            EventBeingChangeActionPoints?.Invoke();
        }
        public void BeingChangeHealth()
        {
            EventBeingChangeHealth?.Invoke();
        }
    }

}
