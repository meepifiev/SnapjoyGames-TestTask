using System;
using Project.Scripts.Runtime.Core.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Runtime.Infrastructure.Factory
{
    public class ObjectFactory : IObjectFactory
    {
        private readonly IObjectResolver _objectResolver;

        public ObjectFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
        }

        public T Create<T>(T prefab, Vector3 position, Quaternion rotation)
            where T : Component
        {
            return _objectResolver.Instantiate(prefab, position, rotation);
        }
    }
}
