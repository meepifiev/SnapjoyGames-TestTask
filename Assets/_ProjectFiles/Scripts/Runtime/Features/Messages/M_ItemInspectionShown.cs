using Project.Scripts.Runtime.Features.Interaction.Items;

namespace Project.Scripts.Runtime.Features.Messages
{
    public readonly struct M_ItemInspectionShown
    {
        public M_ItemInspectionShown(ItemDefinition definition)
        {
            Definition = definition;
        }

        public ItemDefinition Definition { get; }
    }
}
