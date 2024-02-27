using System;
using System.Linq;
using AFSInterview.Items.Interfaces;
using UnityEngine.Events;

namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	[System.Serializable]
	public class MoneyChangedEvent : UnityEvent<int>
	{
	}
	
	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;

		public int Money => money;
		public int ItemsCount => items.Count;
		
		#region Events
		public MoneyChangedEvent OnMoneyAmountChanged = new(); // alternatively we can just use c# delegates/event but this is assignable from editor ui
		#endregion

		public void SellAllItemsUpToValue(int maxValue)
		{
			// doing it from 0 to count will not work when removing during iteration - items above i are shifted after removal
			// doing it from count to 0 works because we dont care about items shifting above our index if we are iterating downwards 
			int moneyBeforeSelling = money;
			for (var i = items.Count - 1; i >= 0; i--)
			{
				var itemValue = items[i].Value;
				if (itemValue > maxValue)
					continue;
				
				money += itemValue;
				items.RemoveAt(i);
			}
			
			if(moneyBeforeSelling != money)
				OnMoneyAmountChanged?.Invoke(money);
		}

		public void AddItem(Item item)
		{
			items.Add(item);
		}

		public void RemoveItem(Item item)
		{
			items.Remove(item);
		}

		public void AddMoney(int value)
		{
			money += value;
			OnMoneyAmountChanged?.Invoke(money);
		}

		[InspectorButton]
		public void ConsumeAllConsumables()
		{
			for (var i = items.Count - 1; i >= 0; i--)
			{
				var item = items[i];
				foreach (var trait in item.Traits)
				{
					if (trait is not IItemConsumableTrait consumable)
						continue;
					consumable.Use(item, this);
				}
			}
		}
	}
}