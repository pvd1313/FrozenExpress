namespace FrozenExpress.Game.Common.GameCycle
{
    public interface IInitializable
    {
        EInitializationOrder InitializationOrder { get; }

        void Initialize();
    }
}
