using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

namespace DarkTunnels.TrackManagment
{
    public class Tunnel : MonoBehaviour
    {
        [field: Space, Header("Path refrence")]
        [field: SerializeField]
        public CinemachinePath Path { get; set; }

        [field: Space, Header("EndTunnel refrence")]
        [field: SerializeField]
        public Transform EndTunnel { get; set; }

        [field: Space, Header("NavMeshSurface refrence")]
        [field: SerializeField]
        public NavMeshSurface NavMesh { get; set; }
    }
}