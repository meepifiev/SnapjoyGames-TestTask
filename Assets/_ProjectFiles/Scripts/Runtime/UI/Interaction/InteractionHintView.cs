using Project.Scripts.Runtime.UI.Common;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Runtime.UI.Interaction
{
    public class InteractionHintView : MonoBehaviour, IInteractionHintView
    {
        [SerializeField] private CanvasGroupVisibility _visibility;
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            Hide();
        }

        public void Show(string text)
        {
            _text.text = text;
            _visibility.Show();
        }

        public void Hide()
        {
            _text.text = string.Empty;
            _visibility.Hide();
        }
    }
}
