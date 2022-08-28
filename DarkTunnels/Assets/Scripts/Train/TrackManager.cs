using UnityEngine;
using Cinemachine;

namespace DarkTunnels
{
    public class TrackManager : MonoBehaviour
    {
        public static TrackManager CurrentTrackManager { get; set; }

        [field: Space, Header("Path")]
        [field: SerializeField]
        private CinemachinePath Path { get; set; }
        [field: SerializeField]
        private Tunnel StraightPathPrefab { get; set; }
        [field: SerializeField]
        private Tunnel TurnLeftPathPrefab { get; set; }
        [field: SerializeField]
        private Tunnel TurnRightPathPrefab { get; set; }

        [field: Space, Header("Add track input")]
        [field: SerializeField]
        private KeyCode AddTrackInput { get; set; }

        [field: Space, Header("Generator setttings")]
        [field: SerializeField]
        private int Distance { get; set; }

        private CinemachinePath.Waypoint[] WaypointsCollection { get; set; }
        private int CurrentWaypointIndex { get; set; }
        private Transform PathSpawn { get; set; }
        private Tunnel CurrentTunnel { get; set; }
        private int PathCollectionIndex { get; set; }
        private bool WasLastTurnLeft { get; set; }

        protected virtual void Awake ()
        {
            SetSingleton();
            Initialize();
        }

        private void SetSingleton ()
        {
            if (CurrentTrackManager != null && CurrentTrackManager != this)
            {
                Destroy(gameObject);
            }

            CurrentTrackManager = this;
        }

        private void Initialize ()
        {
            PathSpawn = transform;

            GenerateTrack();

            CurrentWaypointIndex = 0;
            WaypointsCollection = new CinemachinePath.Waypoint[transform.childCount * 5];

            for (int i = 0; i< transform.childCount; i++)
            {
                Transform currentChild = transform.GetChild(i);

                AddWaypoint(currentChild);
            }

            Path.m_Waypoints = WaypointsCollection;
        }

        private void GenerateTrack ()
        {
            for (int i=0; i<Distance; i++)
            {
                AddTrack();
            }
        }

        private void AddTrack ()
        {
            int whichTrack = Random.Range(0, 2);

            if (whichTrack == 0)
            {
                CurrentTunnel = Instantiate(StraightPathPrefab, PathSpawn.position, PathSpawn.rotation, transform);
            }
            else
            {
                if (WasLastTurnLeft)
                {
                    CurrentTunnel = Instantiate(TurnRightPathPrefab, PathSpawn.position, PathSpawn.rotation, transform);
                    WasLastTurnLeft = false;
                }
                else
                {
                    CurrentTunnel = Instantiate(TurnLeftPathPrefab, PathSpawn.position, PathSpawn.rotation, transform);
                    WasLastTurnLeft = true;
                }
            }

            PathSpawn = CurrentTunnel.EndTunnel;
        }

        private void AddWaypoint(Transform child)
        {
            Tunnel tunnel = child.GetComponent<Tunnel>();
            CinemachinePath path = tunnel.Path;
            int lenghtWaypoints = path.m_Waypoints.Length;

            for (int index = 0; index < lenghtWaypoints; index++)
            {
                CinemachinePath.Waypoint wp = path.m_Waypoints[index];
                CinemachinePath.Waypoint targetWp = new CinemachinePath.Waypoint();

                targetWp.position = child.localRotation * wp.position + child.localPosition;
                targetWp.tangent = child.localRotation * wp.tangent;
                targetWp.roll = wp.roll;

                WaypointsCollection[CurrentWaypointIndex] = targetWp;
                CurrentWaypointIndex++;
            }
        }
    }
}