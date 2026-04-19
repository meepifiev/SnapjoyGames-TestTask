using UnityEngine;

namespace Project.Scripts.Runtime.UI.Common
{
    public class CanvasGroupVisibility : MonoBehaviour
    {
        private const float VisibleAlpha = 1f;
        private const float HiddenAlpha = 0f;

        [SerializeField] private CanvasGroup _canvasGroup;

        public void Show()
        {
            SetVisible(true);
        }

        public void Hide()
        {
            SetVisible(false);
        }

        private void SetVisible(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? VisibleAlpha : HiddenAlpha;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }
    }
}
