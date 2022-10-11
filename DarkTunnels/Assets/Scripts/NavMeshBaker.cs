using System.Collections.Generic;
using UnityEngine;

namespace DarkTunnels
{
    public class NavMeshBaker : MonoBehaviour
    {
        public void BakeNavMesh(List<Tunnel> tunnelsList)
        {
                tunnelsList[0].NavMesh.BuildNavMesh();
        }
    }
}
