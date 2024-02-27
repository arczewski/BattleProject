using AFSInterview.Items.Interfaces;
using UnityEngine;

namespace AFSInterview.Items
{
    [CreateAssetMenu(fileName = "ConsumableMoneyRewardTrait", menuName = "Item/ItemBehavior/ConsumableMoneyRewardTrait")]
    public class ConsumableMoneyRewardItemTrait : ItemTrait, IItemConsumableTrait
    {
        [SerializeField] private int amount;

        public void Use(Item item, InventoryController inventoryController)
        {
            inventoryController.RemoveItem(item);
            inventoryController.AddMoney(amount);
        }
    }
}