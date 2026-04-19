namespace Project.Scripts.Runtime.Features.Interaction.Items
{
    public interface IItemInspectionOutput
    {
        void Show(ItemDefinition definition);
        void Hide();
    }
}
