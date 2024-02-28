using System.Collections.Generic;
using AFSInterview.Items.Interfaces;

namespace AFSInterview.Items
{
	using System;
	using UnityEngine;

	// Flyweight - scriptable objects in unity are representation of Flyweight pattern
	// we can share single instance of item or weapon across many places in the code and thanks to that saving system memory
	[CreateAssetMenu(fileName = "Item", menuName = "Item/New")]
	public class Item : ScriptableObject
	{
		[SerializeField] private string name;
		[SerializeField] private int value;
		[SerializeField] private List<ItemTrait> traits;

		public string Name => name;
		public int Value => value;
		public List<ItemTrait> Traits => traits;

		public Item(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

		public void Use()
		{
			Debug.Log("Using" + Name);
		}
	}
}