using System.Collections.Generic;
using UnityEngine;

namespace DarkTunnels.Utilities
{
    public class ObjectPooler : SingletonMonoBehaviour<ObjectPooler>
    {
        [field: Space, Header("Pools Collection")]
        [field: SerializeField]
        private List<Pool> Pools { get; set; }
        [field: SerializeField]
        private Dictionary<string, Queue<GameObject>> PoolDictionary { get; set; }

        public GameObject SpawnFromPool(string id, Vector3 position, Quaternion rotation)
        {
            GameObject objectToSpawn = PoolDictionary[id].Dequeue();
            objectToSpawn.transform.SetParent(null);
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
            
            if (pooledObject != null)
            {
                pooledObject.ObjectPooled();
            }

            PoolDictionary[id].Enqueue(objectToSpawn);
            return objectToSpawn;
        }

        public GameObject DespawnToPool (string id, GameObject objectToDespawn)
        {
            objectToDespawn.SetActive(false);
            
            foreach (Pool pool in Pools)
            {
                if (pool.ID.ToString() == id)
                {
                    objectToDespawn.transform.SetParent(pool.ObjectPool);
                }
            }

            return objectToDespawn;
        }

        protected virtual void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in Pools)
            {
                Queue<GameObject> objectPool = new();

                for (int i = 0; i < pool.Size; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    obj.transform.SetParent(pool.ObjectPool);
                    objectPool.Enqueue(obj);
                }

                PoolDictionary.Add(pool.ID.ToString(), objectPool);
            }
        }
    }
}
