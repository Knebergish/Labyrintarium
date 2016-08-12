namespace TestOpenGL
{
    interface IParameterable : IFeatureble, IStateble
    {
        double CurrentHealth { get; set; }
        double CurrentActionPoints { get; set; }

        event TEventDelegate<IParameterable> ChangeCurrentHealthEvent;
        event TEventDelegate<IParameterable> ChangeCurrentActionPointsEvent;
        event VoidEventDelegate EndHealthEvent;
        event VoidEventDelegate EndActionPointsEvent;
    }
}
