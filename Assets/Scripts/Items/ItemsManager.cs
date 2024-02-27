using System.Collections;
using System.Collections.Generic;
using AFSInterview.Items.Interfaces;
using UnityEngine.InputSystem;

namespace AFSInterview.Items
{
	using TMPro;
	using UnityEngine;

	public class ItemsManager : MonoBehaviour
	{
		[SerializeField] private InventoryController inventoryController;
		[SerializeField] private ObjectPool objectPool;
		[SerializeField] private int itemSellMaxValue;
		[SerializeField] private Transform itemSpawnParent;
		[SerializeField] private BoxCollider itemSpawnArea;
		[SerializeField] private float itemSpawnInterval;
		[SerializeField] private ObjectType[] spawnableObjectTypes;

		[Space(10)] [Header("UI")] [SerializeField]
		private TextMeshProUGUI _moneyTextMesh;
		
		private int itemPhysicLayer;
		private Camera playerCamera;
		private Coroutine spawningCoroutine;

		private void Awake()
		{
			itemPhysicLayer = LayerMask.GetMask("Item");
			_moneyTextMesh = FindObjectOfType<TextMeshProUGUI>();
			playerCamera = Camera.main;
		}

		private void OnEnable()
		{
			inventoryController.OnMoneyAmountChanged.AddListener(OnMoneyAmountChanged);
			OnMoneyAmountChanged(inventoryController.Money);
			spawningCoroutine = StartCoroutine(SpawnItemRoutine());
		}

		private void OnDisable()
		{
			inventoryController.OnMoneyAmountChanged.RemoveListener(OnMoneyAmountChanged);
			StopCoroutine(spawningCoroutine);
		}

		private void OnMoneyAmountChanged(int money)
		{
			_moneyTextMesh.text = "Money: " + inventoryController.Money;
		}
		
		private IEnumerator SpawnItemRoutine() {
			while(true) {
				yield return new WaitForSeconds(itemSpawnInterval);
				SpawnNewItem();
			}
		}

		public void OnSell(InputAction.CallbackContext value)
		{
			inventoryController.SellAllItemsUpToValue(itemSellMaxValue);
		}

		public void OnMouseLeftClick(InputAction.CallbackContext value)
		{
			if(value.action.IsPressed())
				TryPickUpItem(value.action.ReadValue<Vector2>());
		}

		private void SpawnNewItem()
		{
			var spawnAreaBounds = itemSpawnArea.bounds;
			var position = new Vector3(
				Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
				0f,
				Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
			);

			var newItem = objectPool.GetOrCreate(spawnableObjectTypes[UnityEngine.Random.Range(0, spawnableObjectTypes.Length)]);
			var newItemTransform = newItem.transform;
			newItemTransform.SetParent(itemSpawnParent);
			newItemTransform.position = position;
			newItemTransform.rotation = Quaternion.identity;
		}

		private void TryPickUpItem(Vector2 clickPosition)
		{
			var ray = playerCamera.ScreenPointToRay(clickPosition);
			if (!Physics.Raycast(ray, out var hit, 100f, itemPhysicLayer) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return;
			
			var item = itemHolder.GetItem();
            inventoryController.AddItem(item);
            objectPool.ReturnObject(hit.collider.gameObject);
            
            // In production environment we could have custom preprocessor conditions to disable logging - for example #if !PRODUCTION_ENV
            // Or add a wrapper around Debug.Log to have version for file/remote log aggregation and manage log level from there
            Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + inventoryController.ItemsCount + " items");
		}
	}
}