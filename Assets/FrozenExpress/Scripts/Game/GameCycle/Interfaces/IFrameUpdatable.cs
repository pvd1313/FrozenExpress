namespace FrozenExpress.Game.GameCycle
{
    public interface IFrameUpdatable
    {
        EFrameUpdateOrder UpdateOrder { get; }

        void Update(FrameUpdateData frameData);
    }
}
