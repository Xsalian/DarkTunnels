using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DarkTunnels.Utilities;

namespace DarkTunnels.TrackManagment
{
    public class TrackManager : SingletonMonoBehaviour<TrackManager>
    {
        [field: Header("Path")]
        [field: SerializeField]
        private CinemachinePath Path { get; set; }
        [field: SerializeField]
        private Tunnel StraightPathPrefab { get; set; }
        [field: SerializeField]
        private Tunnel TurnLeftPathPrefab { get; set; }
        [field: SerializeField]
        private Tunnel TurnRightPathPrefab { get; set; }
        [field: SerializeField]
        private Transform EndTrackPoint { get; set; }

        [field: Header("NavMeshBaker reference")]
        [field: SerializeField]
        private NavMeshBaker CurrentNavMeshBaker { get; set; }

        [field: Header("Generator setttings")]
        [field: SerializeField]
        private int Distance { get; set; }
        [field: SerializeField]
        private float Height { get; set; }

        public static TrackManager CurrentTrackManager { get; set; }
        private CinemachinePath.Waypoint[] WaypointsCollection { get; set; }
        private int CurrentWaypointIndex { get; set; }
        private Transform PathSpawn { get; set; }
        private Tunnel CurrentTunnel { get; set; }
        private bool WasLastTurnLeft { get; set; }
        private Vector3 SpawnPosition { get; set; }
        private int NumberStraightPrefabs { get; set; }
        private int NumberTurnPrefabs { get; set; }
        private List<Tunnel> TunnelsCollection { get; set; } = new();

        protected override void Awake ()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize ()
        {
            PathSpawn = transform;
            SpawnPosition = new Vector3(PathSpawn.position.x, Height, PathSpawn.position.z);

            GenerateTrack();
            CurrentNavMeshBaker.BakeNavMesh(TunnelsCollection);

            CurrentWaypointIndex = 0;
            WaypointsCollection = new CinemachinePath.Waypoint[NumberStraightPrefabs * 2 + NumberTurnPrefabs * 5];

            for (int index = 0; index < transform.childCount; index++)
            {
                Transform currentChild = transform.GetChild(index);
                AddWaypoint(currentChild);
            }

            Path.m_Waypoints = WaypointsCollection;

            EndTrackPoint.position = SpawnPosition;
        }

        private void GenerateTrack ()
        {
            for (int index = 0; index < Distance; index++)
            {
                AddTrack();
            }
        }

        private void AddTrack ()
        {
            int whichTrack = Random.Range(0, 2);

            if (whichTrack == 0)
            {
                NumberStraightPrefabs++;
                CurrentTunnel = Instantiate(StraightPathPrefab, SpawnPosition, PathSpawn.rotation, transform);
            }
            else
            {
                NumberTurnPrefabs++;

                if (WasLastTurnLeft == true)
                {
                    CurrentTunnel = Instantiate(TurnRightPathPrefab, SpawnPosition, PathSpawn.rotation, transform);
                    WasLastTurnLeft = false;
                }
                else
                {
                    CurrentTunnel = Instantiate(TurnLeftPathPrefab, SpawnPosition, PathSpawn.rotation, transform);
                    WasLastTurnLeft = true;
                }
            }

            PathSpawn = CurrentTunnel.EndTunnel;
            TunnelsCollection.Add(CurrentTunnel);
            SpawnPosition = new Vector3(PathSpawn.position.x, Height, PathSpawn.position.z);
        }

        private void AddWaypoint (Transform child)
        {
            Tunnel tunnel = child.GetComponent<Tunnel>();
            CinemachinePath path = tunnel.Path;
            int lenghtWaypoints = path.m_Waypoints.Length;

            for (int index = 0; index < lenghtWaypoints; index++)
            {
                CinemachinePath.Waypoint wp = path.m_Waypoints[index];
                CinemachinePath.Waypoint targetWp = new CinemachinePath.Waypoint();

                targetWp.position = child.localRotation * wp.position + child.localPosition;
                targetWp.position.y = 0;
                targetWp.tangent = child.localRotation * wp.tangent;
                targetWp.roll = wp.roll;

                WaypointsCollection[CurrentWaypointIndex] = targetWp;
                CurrentWaypointIndex++;
            }
        }
    }
}