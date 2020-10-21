namespace FrozenExpress.Game.GameCycle
{
    public interface IInitializable
    {
        EInitializationOrder InitializationOrder { get; }

        void Initialize();
    }
}
