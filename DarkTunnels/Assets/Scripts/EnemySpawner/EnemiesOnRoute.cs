using UnityEngine;

namespace DarkTunnels
{
    [CreateAssetMenu(menuName = "DarkTunnels/EnemiesOnRoute")]
    public class EnemiesOnRoute : ScriptableObject
    {
        [field: SerializeField]
        public EnemyController[] EnemyTypeCollection { get; private set; }
    }
}