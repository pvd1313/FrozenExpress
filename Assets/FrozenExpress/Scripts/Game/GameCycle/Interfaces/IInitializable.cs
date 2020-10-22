using FrozenExpress.Scripts.Game.GameCycle.Enums;

namespace FrozenExpress.Scripts.Game.GameCycle.Interfaces
{
    public interface IInitializable
    {
        EInitializationOrder InitializationOrder { get; }

        void Initialize();
    }
}
