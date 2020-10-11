namespace FrozenExpress.Game.Common.GameCycle
{
    public interface IFrameUpdatable
    {
        EFrameUpdateOrder UpdateOrder { get; }

        void Update(FrameUpdateData frameData);
    }
}
