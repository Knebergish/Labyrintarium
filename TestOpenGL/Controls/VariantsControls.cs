namespace TestOpenGL.Controls
{
    class VariantsControls
    {
        public static void StandartGamerControl()
        {
            GlobalData.WorldData.Control.ClearAllActionsControl();
            
            GlobalData.WorldData.Control.AddNewActionControl('a', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Move(TestOpenGL.Direction.Left);
            });
            GlobalData.WorldData.Control.AddNewActionControl('w', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Move(TestOpenGL.Direction.Up);
            });
            GlobalData.WorldData.Control.AddNewActionControl('d', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Move(TestOpenGL.Direction.Right);
            });
            GlobalData.WorldData.Control.AddNewActionControl('s', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Move(TestOpenGL.Direction.Down);
            });
            GlobalData.WorldData.Control.AddNewActionControl('e', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Use();
            });

            GlobalData.WorldData.Control.AddNewActionControl('j', AdditionalKey.None, delegate
            {
                GlobalData.Sight.Move(Direction.Left);
            });
            GlobalData.WorldData.Control.AddNewActionControl('i', AdditionalKey.None, delegate
            {
                GlobalData.Sight.Move(Direction.Up);
            });
            GlobalData.WorldData.Control.AddNewActionControl('l', AdditionalKey.None, delegate
            {
                GlobalData.Sight.Move(Direction.Right);
            });
            GlobalData.WorldData.Control.AddNewActionControl('k', AdditionalKey.None, delegate
            {
                GlobalData.Sight.Move(Direction.Down);
            });
            GlobalData.WorldData.Control.AddNewActionControl(' ', AdditionalKey.None, delegate
            {
                GlobalData.GCycle.Gamer.Attack(GlobalData.Sight.Coord);
            });
            

            GlobalData.WorldData.Control.AddNewActionControl('i', AdditionalKey.Shift, delegate
            {
                GlobalData.FA.ShowInventory();
            });
            GlobalData.WorldData.Control.AddNewActionControl('m', AdditionalKey.Shift, delegate
            {
                GlobalData.FA.ShowMapEditor();
            });
        }
    }
}
