namespace Project.Scripts.Runtime.Core.Time
{
    public interface ITimeProvider
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
        float Time { get; }
    }
}
