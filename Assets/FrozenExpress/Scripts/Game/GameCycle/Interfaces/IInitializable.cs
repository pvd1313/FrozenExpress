namespace FrozenExpress.Game
{
    public interface IInitializable
    {
        EInitializationOrder InitializationOrder { get; }

        void Initialize();
    }
}
