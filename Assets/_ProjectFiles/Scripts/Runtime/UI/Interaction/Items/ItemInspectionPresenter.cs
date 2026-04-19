using System;
using Project.Scripts.Runtime.Features.Interaction.Items;

namespace Project.Scripts.Runtime.UI.Interaction.Items
{
    public class ItemInspectionPresenter
    {
        private readonly IItemInspectionView _view;

        public ItemInspectionPresenter(IItemInspectionView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Show(ItemDefinition definition)
        {
            _view.Show(definition.DisplayName, definition.Description);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}
