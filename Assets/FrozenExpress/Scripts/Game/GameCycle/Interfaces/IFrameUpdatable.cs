using FrozenExpress.Scripts.Game.GameCycle.Data;
using FrozenExpress.Scripts.Game.GameCycle.Enums;

namespace FrozenExpress.Scripts.Game.GameCycle.Interfaces
{
    public interface IFrameUpdatable
    {
        EFrameUpdateOrder UpdateOrder { get; }

        void Update(FrameUpdateData frameData);
    }
}
