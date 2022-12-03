using UnityEngine;

namespace DarkTunnels
{
    [CreateAssetMenu(menuName = "DarkTunnels/EnemyStatistiscData")]
    public class EnemyStatisticsData : ScriptableObject
    {
        [field: SerializeField]
        public int MaxHealthPoints { get; private set; }
        [field: SerializeField]
        public int Speed { get; private set; }
        [field: SerializeField]
        public int AttackPower { get; private set; }
    }
}