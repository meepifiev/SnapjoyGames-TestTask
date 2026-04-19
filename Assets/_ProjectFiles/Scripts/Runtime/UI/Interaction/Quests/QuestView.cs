using Project.Scripts.Runtime.Features.Interaction.Quests;
using Project.Scripts.Runtime.UI.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Runtime.UI.Interaction.Quests
{
    public class QuestView : MonoBehaviour, IQuestView
    {
        [SerializeField] private CanvasGroupVisibility _visibility;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Toggle _checkbox;

        private void Awake()
        {
            _visibility.Hide();
            _checkbox.interactable = false;
        }

        public void Show(QuestViewData viewData)
        {
            _description.text = viewData.Description;
            _checkbox.isOn = viewData.IsCompleted;
            _visibility.Show();
        }
    }
}
