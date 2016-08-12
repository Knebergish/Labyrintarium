using System;
using System.Collections.Generic;

using TestOpenGL.Logic;
using TestOpenGL.Renders;
using TestOpenGL.PhisicalObjects.ChieldsItem;


namespace TestOpenGL.PhisicalObjects
{
    class Being : PhisicalObject
    {
        bool isSpawned;
        int rangeOfVisibility;

        IParameterable parameters;
        IInventoryble inventory;

        event VoidEventDelegate deathEvent;
        event VoidEventDelegate startStepEvent;
        event VoidEventDelegate endStepEvent;
        event VoidEventDelegate endActionEvent;
        //-------------


        public Being(Being being)
            : this(being.GraphicObjectsPack, being.ObjectInfo, being.Alliance) { }
        public Being(GraphicObjectsPack graphicObjectPack, ObjectInfo objectInfo, int alliance)
            : this(graphicObjectPack, objectInfo, alliance, null, null) { }
        public Being(GraphicObjectsPack graphicObjectPack, ObjectInfo objectInfo, int alliance, IInventoryble inventory, IParameterable parameters)
            : base(Layer.Being, graphicObjectPack, objectInfo)
        {
            NewPositionCheck += Program.L.IsPassable;

            isSpawned = false;
            rangeOfVisibility = 10;
            
            Bag bag = new Bag(20);
            this.inventory = inventory ?? new StandartInventory(new Equipment(bag), bag);

            this.parameters = parameters ?? new Parameters(new TestFeatures(), this.inventory);
                //TODO: временный костыль
                this.parameters.SetState(State.IncreaseActionPoints, 1);

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
        { get { return isSpawned; } }

        public IParameterable Parameters
        { get { return parameters; } }

        public IInventoryble Inventory
        { get { return inventory; } }

        public event VoidEventDelegate DeathEvent
        {
            add { deathEvent += value; }
            remove { deathEvent -= value; }
        }
        public event VoidEventDelegate StartStepEvent
        {
            add { startStepEvent += value; }
            remove { startStepEvent -= value; }
        }
        public event VoidEventDelegate EndStepEvent
        {
            add { endStepEvent += value; }
            remove { endStepEvent -= value; }
        }
        public event VoidEventDelegate EndActionEvent
        {
            add { endActionEvent += value; }
            remove { endActionEvent -= value; }
        }
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
            deathEvent?.Invoke();
        }

        public void Step()
        {
            startStepEvent?.Invoke();
            while (!CheckEndStep())
            {
                Action();
                endActionEvent?.Invoke();
            }
            endStepEvent?.Invoke();
        }

        protected virtual void Action() { }

        bool CheckEndStep()
        {
            return parameters.CurrentActionPoints < 1 ? true : false;
        }
        
        public bool Move(Coord coord)
        {
            if(isSpawned && parameters.CurrentActionPoints >= 1 && SetNewPosition(0, coord))
            {
                Program.L.Pause(100);
                parameters.CurrentActionPoints--;
                return true;
            }
            return false;
        }
        public bool Move(Direction course)
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
                parameters.CurrentHealth -= count;
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
                parameters.CurrentHealth += count;
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
            Weapon weapon = (Weapon)inventory.GetEquipedItem(Section.Weapon);
            if (weapon == null)
                return false;

            if (
                weapon.MinDistance <= Analytics.Distance(Coord, coord)
                && weapon.MaxDistance >= Analytics.Distance(Coord, coord)
                )
                if (Battle.Attack(this, coord))
                {
                    parameters.CurrentActionPoints--;
                    return true;
                }
            return false;
        }

        public void Increace()
        {
            parameters.CurrentActionPoints += parameters[State.IncreaseActionPoints];
            parameters.CurrentHealth += parameters[State.IncreaseHealth];
        }
    }

    /*class EventsBeing
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
    }*/

}
