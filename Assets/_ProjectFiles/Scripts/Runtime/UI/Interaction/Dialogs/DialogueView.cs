using Project.Scripts.Runtime.Features.Interaction.Dialogs;
using Project.Scripts.Runtime.UI.Common;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Runtime.UI.Interaction.Dialogs
{
    public class DialogueView : MonoBehaviour, IDialogueView
    {
        [SerializeField] private CanvasGroupVisibility _visibility;
        [SerializeField] private TMP_Text _speakerName;
        [SerializeField] private TMP_Text _line;
        [SerializeField] private Transform _choiceContainer;
        [SerializeField] private DialogueChoiceButton _choiceButtonPrefab;

        private void Awake()
        {
            Hide();
        }

        public void Show(DialogueViewData viewData)
        {
            _speakerName.text = viewData.SpeakerName;
            _line.text = viewData.Line;

            ClearChoices();

            foreach (DialogueChoiceViewData choice in viewData.Choices)
            {
                DialogueChoiceButton choiceButton = Instantiate(_choiceButtonPrefab, _choiceContainer);
                choiceButton.Set(choice.Text, choice.Select);
            }

            _visibility.Show();
        }

        public void Hide()
        {
            _speakerName.text = string.Empty;
            _line.text = string.Empty;

            ClearChoices();
            _visibility.Hide();
        }

        private void ClearChoices()
        {
            for (int childIndex = _choiceContainer.childCount - 1; childIndex >= 0; childIndex--)
                Destroy(_choiceContainer.GetChild(childIndex).gameObject);
        }
    }
}
