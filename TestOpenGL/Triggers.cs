using System;
using System.Collections.Generic;

namespace TestOpenGL
{
    class Triggers
    {
        public static Triggers currentTriggers;
        private List<Trigger> triggers;

        public Triggers()
        {
            Triggers.currentTriggers = this;
            triggers = new List<Trigger>();
        }

        public void AddTrigger(Trigger trigger)
        {
            this.triggers.Add(trigger);
        }

        public void CallAllTriggers()
        {
            foreach (Trigger t in triggers)
                t.CallAction();
        }

        public void ActivateTrigger(int number)
        {
            for (int i = 0; i < triggers.Count; i++)
                if (triggers[i].Number == number)
                    triggers[i].Active = true;
        }
        public void DeactivateTrigger(int number)
        {
            for (int i = 0; i < triggers.Count; i++)
                if (triggers[i].Number == number)
                    triggers[i].Active = false;
        }
        
    }

    class Trigger
    {
        int number;
        bool active;
        //TODO: может вынести в Structures?
        public delegate void Action();
        private Action action;

        public int Number
        {
            get { return number; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        private Trigger() { }
        public Trigger(int number, bool active, Action action)
        {
            this.number = number;
            this.active = active;
            this.action = action;
        }

        public void CallAction()
        {
            if (Active)
                action();
        }
    }
}
