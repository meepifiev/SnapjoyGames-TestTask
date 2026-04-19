using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public class NoteInspectionAnimation : MonoBehaviour, IItemInspectionAnimation
    {
        [SerializeField] private NoteInspectionAnimationSettings _settings;
        [SerializeField] private Transform _leftPage;
        [SerializeField] private Transform _rightPage;

        private Quaternion _leftPageClosedRotation;
        private Quaternion _rightPageClosedRotation;
        
        private Tween _animationTween;

        private void Awake()
        {
            _leftPageClosedRotation = _leftPage.localRotation;
            _rightPageClosedRotation = _rightPage.localRotation;
        }

        private void OnDestroy()
        {
            _animationTween?.Kill();
        }

        public void Open()
        {
            Play(
                _leftPageClosedRotation * Quaternion.Euler(_settings.LeftPageOpenLocalEulerAngles),
                _rightPageClosedRotation * Quaternion.Euler(_settings.RightPageOpenLocalEulerAngles));
        }

        public void Close()
        {
            Play(_leftPageClosedRotation, _rightPageClosedRotation);
        }

        private void Play(Quaternion leftPageRotation, Quaternion rightPageRotation)
        {
            _animationTween?.Kill();

            _animationTween = DOTween.Sequence()
                .Join(_leftPage.DOLocalRotateQuaternion(leftPageRotation, _settings.Duration))
                .Join(_rightPage.DOLocalRotateQuaternion(rightPageRotation, _settings.Duration))
                .SetEase(_settings.Ease);
        }
    }
}
