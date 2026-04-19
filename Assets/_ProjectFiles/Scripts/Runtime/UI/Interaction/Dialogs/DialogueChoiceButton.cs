using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Runtime.UI.Interaction.Dialogs
{
    public class DialogueChoiceButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _label;

        private Action _select;

        public void Set(string text, Action select)
        {
            _label.text = text;
            _select = select;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _select?.Invoke();
        }
    }
}
