using UnityEngine;
using Cinemachine;

namespace DarkTunnels
{
    public class Tunnel : MonoBehaviour
    {
        [field: Space, Header("Path refrence")]
        [field: SerializeField]
        private CinemachinePath Path { get; set; }

        [field: Space, Header("EndTunnel refrence")]
        [field: SerializeField]
        private Transform EndTunnel { get; set; }
    }
}