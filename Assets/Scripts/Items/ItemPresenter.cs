using AFSInterview.Items.Interfaces;

namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		[SerializeField] private Item item;
        
		public Item GetItem()
		{
			// I prefer methods not to do too much if it is not specified in its name
			// I didn't expect GetItem to remove something
			// Giving back control to item manager - alternatively splitting it into Get and Remove methods would also be fine
			return item;
		}
	}
}