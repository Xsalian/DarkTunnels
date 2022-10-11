using UnityEngine;
using UnityEngine.AI;

namespace DarkTunnels 
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField]
        private Transform Player { get; set; }
        [field: SerializeField]
        private NavMeshAgent EnemyAgent { get; set; }

        void Update()
        {
            EnemyAgent.SetDestination(Player.position);
        }
    }
}
