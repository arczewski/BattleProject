using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private PoolObjectDefinition[] _poolObjectDefinitions;
        private Dictionary<ObjectType, Queue<GameObject>> _pools = new Dictionary<ObjectType, Queue<GameObject>>();

        private void Start()
        {
            InitializeObjectPool();
        }

        private void InitializeObjectPool()
        {
            foreach (var definition in _poolObjectDefinitions)
            {
                // we could check if we don't have duplicates in _poolObjectDefinitions but it is ok if it throws exception for us to fix by updating _poolObjectDefinitions and removing any duplicate
                var queue = new Queue<GameObject>();
                _pools.Add(definition.ObjectType, queue);
                if(definition.PreSpawnCount == 0)
                    continue;
                for (int i = 0; i < definition.PreSpawnCount; i++)
                {
                    var preSpawnedObject = Instantiate(definition.Prefab, this.transform);
                    preSpawnedObject.SetActive(false);
                    preSpawnedObject.AddComponent<PooledObject>().ObjectType = definition.ObjectType;
                    queue.Enqueue(preSpawnedObject);
                }
            }
        }

        public GameObject GetOrCreate(ObjectType objectType)
        {
            if (!_pools.TryGetValue(objectType, out var cachedObjectQueue))
                throw new ArgumentException($"Missing prefab for {objectType}");

            if (cachedObjectQueue.Count == 0)
            {
                // could be improved to just copy _poolObjectDefinitions to dictionary and use O(1) access time instead of O(n)
                // but we should aim to have enough object in pool anyway so this should not be common code path
                var newObject = Instantiate(_poolObjectDefinitions.Single(x => x.ObjectType == objectType).Prefab);
                if(!newObject.activeSelf)
                    newObject.SetActive(true);
                return newObject;
            }

            var pooledObject = cachedObjectQueue.Dequeue();
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnObject(GameObject obj)
        {
            if (!obj.TryGetComponent<PooledObject>(out var pooledObject))
                throw new ArgumentException($"Missing PooledObject component"); // or we could Destroy(obj);
            if (!_pools.TryGetValue(pooledObject.ObjectType, out var cachedObjectQueue))
                throw new ArgumentException($"Missing prefab for {pooledObject.ObjectType}"); // or we could Destroy(obj);
            obj.SetActive(false);
            cachedObjectQueue.Enqueue(obj);
        }
    }

    public enum ObjectType
    {
        Apple = 0,
        ChestWithApples = 1,
        ChestWithMoney = 2,
        Archer = 3,
    }

    [Serializable]
    public struct PoolObjectDefinition
    {
        public ObjectType ObjectType;
        public GameObject Prefab;
        public int PreSpawnCount;
    }
}