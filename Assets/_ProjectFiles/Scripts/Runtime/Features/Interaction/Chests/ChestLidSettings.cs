using System;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Chests
{
    [Serializable]
    public class ChestLidSettings
    {
        [SerializeField] private Vector3 _openLocalEulerAngles;
        [SerializeField, Min(0)] private float _transitionDuration = 0.5f;
        [SerializeField] private Ease _transitionEase = Ease.Linear;

        public Vector3 OpenLocalEulerAngles => _openLocalEulerAngles;
        public float TransitionDuration => _transitionDuration;
        public Ease TransitionEase => _transitionEase;
    }
}
