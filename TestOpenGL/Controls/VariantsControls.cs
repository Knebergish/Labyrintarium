namespace TestOpenGL.Controls
{
    class VariantsControls
    {
        public static void StandartGamerControl()
        {
            Program.C.ClearAllActionsControl();
            
            Program.C.AddNewActionControl('a', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Left);
            });
            Program.C.AddNewActionControl('w', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Up);
            });
            Program.C.AddNewActionControl('d', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Right);
            });
            Program.C.AddNewActionControl('s', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Move(TestOpenGL.Direction.Down);
            });
            Program.C.AddNewActionControl('e', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Use();
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
            Program.C.AddNewActionControl(' ', AdditionalKeys.None, delegate
            {
                Program.GCycle.Gamer.Attack(Program.P.Camera.Sight.C);
            });
            

            Program.C.AddNewActionControl('i', AdditionalKeys.Shift, delegate
            {
                Program.FA.ShowInventory();
            });
            Program.C.AddNewActionControl('m', AdditionalKeys.Shift, delegate
            {
                Program.FA.ShowMapEditor();
            });
        }
    }
}
