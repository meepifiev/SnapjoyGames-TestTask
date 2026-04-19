using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    [System.Serializable]
    public class HeldItemSettings
    {
        private const float MinTransitionDuration = 0f;

        [SerializeField] private Vector3 _localEulerAngles;
        [SerializeField] private Vector3 _localScale = Vector3.one;
        [SerializeField, Min(MinTransitionDuration)] private float _transitionDuration;
        [SerializeField] private Ease _transitionEase;

        public Vector3 LocalEulerAngles => _localEulerAngles;
        public Vector3 LocalScale => _localScale;
        public float TransitionDuration => _transitionDuration;
        public Ease TransitionEase => _transitionEase;
    }
}
