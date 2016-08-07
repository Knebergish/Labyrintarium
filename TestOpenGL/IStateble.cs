using System.Collections.Generic;


namespace TestOpenGL
{
    interface IStateble
    {
        double this[State state] { get; }
        List<double> GetAllStates();
    }
}
