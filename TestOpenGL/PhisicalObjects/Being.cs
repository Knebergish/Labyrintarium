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
        GraphicObjectsPack equipmentGraphicObjectsPack;

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
            NewPositionCheck += GlobalData.WorldData.Level.IsPassable;

            isSpawned = false;
            rangeOfVisibility = 10;

            equipmentGraphicObjectsPack = new GraphicObjectsPack(this);
            Bag bag = new Bag(20);
            this.inventory = inventory ?? new StandartInventory(new Equipment(bag), bag);
            UpdateEquipmentGraphicObjectsPack(this.inventory);
            this.inventory.ChangeEquipmentEvent += UpdateEquipmentGraphicObjectsPack;

            this.parameters = parameters ?? new Parameters(new TestFeatures(), this.inventory);

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
                GlobalData.WorldData.Level.GetMap<Being>().AddObject(this);
                GlobalData.WorldData.RendereableObjectsContainer.Add(GraphicObjectsPack);
                GlobalData.GCycle.EventStepBeings += Step;
                GlobalData.GCycle.EventStepBeingsIncrease += Increace;
                isSpawned = true;
                return true;
            }
            return false;
        }

        protected override bool IsEmptyPosition(int partLayer, Coord coord)
        {
            return GlobalData.WorldData.Level.GetMap<Being>().GetObject(partLayer, coord) == null ? true : false;
        }

        public override void Despawn()
        {
            isSpawned = false;
            //TODO: проверить работоспособность этого кода
            GlobalData.GCycle.EventStepBeings -= Step;
            GlobalData.GCycle.EventStepBeingsIncrease -= Increace;
            //
            GlobalData.WorldData.Level.GetMap<Being>().RemoveObject(PartLayer, Coord);
            GlobalData.WorldData.RendereableObjectsContainer.Remove(GraphicObjectsPack);
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
                GlobalData.WorldData.Level.Pause(100);
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
                if (GlobalData.WorldData.Level.IsPassable(PartLayer, new Coord(Coord.X + dx, Coord.Y + dy)))
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
                Action removeDecal = GlobalData.WorldData.DecalsAssistant.AddDecal(GlobalData.OB.GetDecal(3), Coord);
                GlobalData.WorldData.Level.Pause(150);
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
            if (Analytics.Distance(Coord, GlobalData.Sight.Coord) > 1)
                return false;

            foreach (Block b in GlobalData.WorldData.Level.GetMap<Block>().GetCellObject(GlobalData.Sight.Coord))
                if (b is IUsable)
                    ((IUsable)b).Used();

            foreach (Being b in GlobalData.WorldData.Level.GetMap<Being>().GetCellObject(GlobalData.Sight.Coord))
                if (b is IUsable)
                    ((IUsable)b).Used();
            return true;
        }

        //TODO: говнокод
        public bool Attack(Coord coord)
        {
            /*Weapon weapon = (Weapon)inventory.GetEquipedItem(Section.Weapon);
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
            return false;*/
            GlobalData.WorldData.Level.GetMap<Being>().GetObject(0, coord)?.Damage(1);
            Parameters.CurrentActionPoints--;
            return true;
        }

        public void Increace()
        {
            parameters.CurrentActionPoints += parameters[State.IncreaseActionPoints];
            parameters.CurrentHealth += parameters[State.IncreaseHealth];
        }

        void UpdateEquipmentGraphicObjectsPack(IEquipmentable equipment)
        {
            GlobalData.WorldData?.RendereableObjectsContainer.Remove(equipmentGraphicObjectsPack);
            ChangeCoordEvent -= equipmentGraphicObjectsPack.UpdatePosition;

            equipmentGraphicObjectsPack = new GraphicObjectsPack(this);
            foreach (Item item in equipment.GetAllEquipmentItems() ?? new List<Item>())
                equipmentGraphicObjectsPack.AddGraphicObject(item.Section.ToString(), ChangePartLayer.No, item.GraphicObject);

            GlobalData.WorldData?.RendereableObjectsContainer.Add(equipmentGraphicObjectsPack);
            ChangeCoordEvent += equipmentGraphicObjectsPack.UpdatePosition;
        }

		public override PhisicalObject Clone()
		{
			return new Being(this);
		}
	}
}
