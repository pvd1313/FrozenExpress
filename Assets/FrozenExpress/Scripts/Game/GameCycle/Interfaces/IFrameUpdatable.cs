namespace FrozenExpress.Game
{
    public interface IFrameUpdatable
    {
        EFrameUpdateOrder UpdateOrder { get; }

        void Update(FrameUpdateData frameData);
    }
}
