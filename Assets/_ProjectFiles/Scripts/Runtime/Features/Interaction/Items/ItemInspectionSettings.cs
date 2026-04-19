using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [Serializable]
    public class ItemInspectionSettings
    {
        private const float MinTransitionDuration = 0f;

        [SerializeField] private Vector3 _localEulerAngles;
        [SerializeField] private Vector3 _localScale = Vector3.one;
        [SerializeField, Min(MinTransitionDuration)] private float _transitionDuration;
        [SerializeField] private Ease _transitionEase;
        [SerializeField] private float _rotationSpeed;

        public Vector3 LocalEulerAngles => _localEulerAngles;
        public Vector3 LocalScale => _localScale;
        public float TransitionDuration => _transitionDuration;
        public Ease TransitionEase => _transitionEase;
        public float RotationSpeed => _rotationSpeed;
    }
}
