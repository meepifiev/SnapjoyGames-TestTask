using UnityEngine;

namespace Project.Scripts.Runtime.Core.Factory
{
    public interface IObjectFactory
    {
        T Create<T>(T prefab, Vector3 position, Quaternion rotation) 
            where T : Component;
    }
}
