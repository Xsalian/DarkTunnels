using UnityEngine;

namespace DarkTunnels.Utilities
{
    [System.Serializable]
    public class Pool
    {
        [field: SerializeField]
        public ObjectID ID { get; set; }
        [field: SerializeField]
        public GameObject Prefab { get; set; }
        [field: SerializeField]
        public int Size { get; set; }
        [field: SerializeField]
        public Transform ObjectPool { get; set; }

        public enum ObjectID
        {

        }
    }
}