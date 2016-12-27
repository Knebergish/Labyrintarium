using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOpenGL.Logic;

namespace TestOpenGL.PhisicalObjects.ChieldsBeing
{
    class DecorBeing : Being
    {
        Coord goal;
        List<Action> deletes;

        public DecorBeing(Being being, Coord goal) :
            base(being)
        {
            //this.goal = goal;
            deletes = new List<Action>();
        }

        protected override void Action()
        {
            GlobalData.RenderManager.StopRender();

	        goal = GlobalData.GCycle.Gamer.Coord;

            Stack<Coord> sc = Analytics.BFS(Coord, goal);

            if (!Move(sc.Count > 0 ? sc.Peek() : Coord))
                Parameters.CurrentActionPoints = 0;
	        Parameters.CurrentActionPoints--;

			foreach (Action action in deletes)
                action();

            deletes.Clear();
            foreach(Coord coord in sc)
            {
                deletes.Add(GlobalData.WorldData.DecalsAssistant.AddDecal(GlobalData.OB.GetDecal(3), coord));
            }

            GlobalData.RenderManager.StartRender();
        }
    }
}