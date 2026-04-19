using Project.Scripts.Runtime.Features.Interaction.Items;

namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    public interface IQuestItemSelector
    {
        PickupItem Select(ItemType[] excludedItemTypes);
    }
}
