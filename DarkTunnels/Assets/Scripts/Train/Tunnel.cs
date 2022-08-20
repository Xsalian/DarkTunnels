using UnityEngine;
using Cinemachine;

namespace DarkTunnels
{
    public class Tunnel : MonoBehaviour
    {
        [field: Space, Header("Path refrence")]
        [field: SerializeField]
        public CinemachinePath Path { get; set; }

        [field: Space, Header("EndTunnel refrence")]
        [field: SerializeField]
        public Transform EndTunnel { get; set; }
    }
}