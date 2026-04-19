using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [Serializable]
    public class ItemInspectionSettings
    {
        [SerializeField] private Vector3 _localEulerAngles;
        [SerializeField] private Vector3 _localScale = Vector3.one;
        [SerializeField, Min(0)] private float _transitionDuration = 0.5f;
        [SerializeField] private Ease _transitionEase = Ease.OutQuad;
        [SerializeField] private float _rotationSpeed = 3f;

        public Vector3 LocalEulerAngles => _localEulerAngles;
        public Vector3 LocalScale => _localScale;
        public float TransitionDuration => _transitionDuration;
        public Ease TransitionEase => _transitionEase;
        public float RotationSpeed => _rotationSpeed;
    }
}
