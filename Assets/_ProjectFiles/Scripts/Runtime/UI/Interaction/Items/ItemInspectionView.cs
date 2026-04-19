using Project.Scripts.Runtime.UI.Common;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Runtime.UI.Interaction.Items
{
    public class ItemInspectionView : MonoBehaviour, IItemInspectionView
    {
        [SerializeField] private CanvasGroupVisibility _visibility;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        
        private void Awake()
        {
            Hide();
        }

        public void Show(string title, string description)
        {
            _title.text = title;
            _description.text = description;
            _visibility.Show();
        }

        public void Hide()
        {
            _title.text = string.Empty;
            _description.text = string.Empty;
            _visibility.Hide();
        }
    }
}
