namespace TestOpenGL.Controls
{
    class VariantsControls
    {
        public static void StandartGamerControl()
        {
            Program.C.ClearAllActionsControl();
            
            Program.C.AddNewActionControl('a', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Left);
            });
            Program.C.AddNewActionControl('w', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Up);
            });
            Program.C.AddNewActionControl('d', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Right);
            });
            Program.C.AddNewActionControl('s', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Down);
            });
            Program.C.AddNewActionControl('e', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Use();
            });

            Program.C.AddNewActionControl('j', AdditionalKey.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Left);
            });
            Program.C.AddNewActionControl('i', AdditionalKey.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Up);
            });
            Program.C.AddNewActionControl('l', AdditionalKey.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Right);
            });
            Program.C.AddNewActionControl('k', AdditionalKey.None, delegate
            {
                Program.P.Camera.Sight.MoveSight(Direction.Down);
            });
            Program.C.AddNewActionControl(' ', AdditionalKey.None, delegate
            {
                Program.GCycle.Gamer.Attack(Program.P.Camera.Sight.Coord);
            });
            

            Program.C.AddNewActionControl('i', AdditionalKey.Shift, delegate
            {
                Program.FA.ShowInventory();
            });
            Program.C.AddNewActionControl('m', AdditionalKey.Shift, delegate
            {
                Program.FA.ShowMapEditor();
            });
        }
    }
}
