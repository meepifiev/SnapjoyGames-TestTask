using System;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.ValveDoors
{
    [Serializable]
    public class ValveDoorMotionSettings
    {
        private const float MinAngle = 1f;
        private const float MinSpeed = 0.01f;

        [SerializeField] private Vector3 _valveRotationAxis;
        [SerializeField, Min(MinAngle)] private float _maxValveAngle;
        [SerializeField, Min(MinSpeed)] private float _openingSpeed;
        [SerializeField, Min(MinSpeed)] private float _returnSpeed;
        [SerializeField] private Vector3 _doorOpenLocalOffset;

        public Vector3 ValveRotationAxis => _valveRotationAxis;
        public float MaxValveAngle => _maxValveAngle;
        public float OpeningSpeed => _openingSpeed;
        public float ReturnSpeed => _returnSpeed;
        public Vector3 DoorOpenLocalOffset => _doorOpenLocalOffset;
    }
}
