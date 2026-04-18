using Project.Scripts.Runtime.Core.Time;

namespace Project.Scripts.Runtime.Infrastructure.Time
{
    public class TimeProvider : ITimeProvider
    {
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;
        public float Time => UnityEngine.Time.time;
    }
}
