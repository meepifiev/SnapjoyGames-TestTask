using TMPro;
using UnityEngine;

namespace Project.Scripts.Runtime.UI.Interaction
{
    public class InteractionHintView : MonoBehaviour, IInteractionHintView
    {
        [SerializeField] private CanvasGroup _root;
        [SerializeField] private TMP_Text _text;
        
        private void Awake()
        {
            Hide();
        }

        public void Show(string text)
        {
            _text.text = text;
            SetVisible(true);
        }

        public void Hide()
        {
            _text.text = string.Empty;
            SetVisible(false);
        }

        private void SetVisible(bool isVisible)
        { 
            const float VisibleAlpha = 1f;
            const float HiddenAlpha = 0f;
        
            _root.alpha = isVisible ? VisibleAlpha : HiddenAlpha;
            _root.interactable = isVisible;
            _root.blocksRaycasts = isVisible;
        }
    }
}
