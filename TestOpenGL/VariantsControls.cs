using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOpenGL
{
    class VariantsControls
    {
        public static void StandartGamerControl()
        {
            Program.C.AddNewActionControl('a', AdditionalKeys.None, delegate
            {
                if (Program.GCycle.Gamer.isSpawned)
                    Program.GCycle.Gamer.Move(TestOpenGL.Direction.Left);
            });
            Program.C.AddNewActionControl('w', AdditionalKeys.None, delegate
            {
                if (Program.GCycle.Gamer.isSpawned)
                    Program.GCycle.Gamer.Move(TestOpenGL.Direction.Up);
            });
            Program.C.AddNewActionControl('d', AdditionalKeys.None, delegate
            {
                if (Program.GCycle.Gamer.isSpawned)
                    Program.GCycle.Gamer.Move(TestOpenGL.Direction.Right);
            });
            Program.C.AddNewActionControl('s', AdditionalKeys.None, delegate
            {
                if (Program.GCycle.Gamer.isSpawned)
                    Program.GCycle.Gamer.Move(TestOpenGL.Direction.Down);
            });

            Program.C.AddNewActionControl('j', AdditionalKeys.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Left);
            });
            Program.C.AddNewActionControl('i', AdditionalKeys.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Up);
            });
            Program.C.AddNewActionControl('l', AdditionalKeys.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Right);
            });
            Program.C.AddNewActionControl('k', AdditionalKeys.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Down);
            });

            Program.C.AddNewActionControl('i', AdditionalKeys.Shift, delegate
            {
                Program.FA.ProcessingOpeningForms(1);
            });
            Program.C.AddNewActionControl('m', AdditionalKeys.Shift, delegate
            {
                Program.FA.ProcessingOpeningForms(2);
            });
        }
    }
}
