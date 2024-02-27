using AFSInterview.Items.Interfaces;
using UnityEngine;

namespace AFSInterview.Items
{
    [CreateAssetMenu(fileName = "ConsumableItemRewardTrait", menuName = "Item/ItemBehavior/ConsumableItemRewardTrait")]
    public class ConsumableItemRewardTrait : ItemTrait, IItemConsumableTrait
    {
        [SerializeField] private Item reward;
        [SerializeField] private int amount;

        public void Use(Item item, InventoryController inventoryController)
        {
            inventoryController.RemoveItem(item);
            for(int i = 0; i < amount; i++)
                inventoryController.AddItem(reward);
        }
    }
}