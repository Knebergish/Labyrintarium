using System;
using System.Collections.Generic;

namespace TestOpenGL
{
    class Triggers
    {
        static Triggers currentTriggers;
        private List<Trigger> triggers;

        public Triggers()
        {
            Triggers.currentTriggers = this;
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
        private delegate void Action();
        private Action action;

        public int Number
        {
            get { return number; }
        }
        public bool Active
        {
            get { return Active; }
            set { Active = value; }
        }

        private Trigger() { }
        public Trigger(int number, bool active, Delegate action)
        {
            this.number = number;
            this.Active = active;
            this.action = (Action)action;
        }

        public void CallAction()
        {
            if (Active)
                action();
        }
    }
}
