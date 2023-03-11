using System.Collections.Generic;
using UnityEngine;

namespace DarkTunnels.TrackManagment
{
    public class NavMeshBaker : MonoBehaviour
    {
        public void BakeNavMesh(List<Tunnel> tunnelsList)
        {
            tunnelsList[0].NavMesh.BuildNavMesh();
        }
    }
}
