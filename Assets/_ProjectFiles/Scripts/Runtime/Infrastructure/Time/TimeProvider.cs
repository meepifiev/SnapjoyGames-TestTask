using Project.Scripts.Runtime.Core.Time;

namespace Project.Scripts.Runtime.Infrastructure.Time
{
    public class TimeProvider : ITimeProvider
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
    }
}
