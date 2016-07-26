namespace TestOpenGL
{
    interface ISpawnable
    {
        bool Spawn(int partLayer, Coord coord);
        void Despawn();
    }
}
