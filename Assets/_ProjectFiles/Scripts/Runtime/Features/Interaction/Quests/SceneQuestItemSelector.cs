using System.Collections.Generic;
using Project.Scripts.Runtime.Features.Interaction.Items;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Runtime.Features.Interaction.Quests
{
    public class SceneQuestItemSelector : IQuestItemSelector
    {
        private readonly List<PickupItem> _candidateItems = new();

        public PickupItem Select(ItemType[] excludedItemTypes)
        {
            _candidateItems.Clear();

            PickupItem[] items = Object.FindObjectsByType<PickupItem>(
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None);

            foreach (PickupItem item in items)
            {
                if (CanSelect(item, excludedItemTypes))
                    _candidateItems.Add(item);
            }

            return _candidateItems.Count == 0 ? null : _candidateItems[Random.Range(0, _candidateItems.Count)];
        }

        private bool CanSelect(PickupItem item, ItemType[] excludedItemTypes)
        {
            return item != null &&
                   item.Definition != null &&
                   IsExcluded(item.Definition.Type, excludedItemTypes) == false;
        }

        private bool IsExcluded(ItemType itemType, ItemType[] excludedItemTypes)
        {
            if (excludedItemTypes == null)
                return false;

            foreach (ItemType excludedType in excludedItemTypes)
            {
                if (excludedType == itemType)
                    return true;
            }

            return false;
        }
    }
}
