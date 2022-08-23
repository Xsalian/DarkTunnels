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
        private Tunnel[] PathPrefabsCollection { get; set; }

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
            GenerateTrack();

            CurrentWaypointIndex = 0;
            WaypointsCollection = new CinemachinePath.Waypoint[transform.childCount + 1];

            for (int i = 0; i< transform.childCount; i++)
            {
                Transform currentChild = transform.GetChild(i);

                if (i == 0)
                {
                    AddWaypoint(currentChild, 0);
                }

                AddWaypoint(currentChild, 1);
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
            PathCollectionIndex = Random.Range(0, PathPrefabsCollection.Length);

            if (PathSpawn == null)
            {
                CurrentTunnel = Instantiate(PathPrefabsCollection[PathCollectionIndex], Vector3.zero, Quaternion.identity, transform);
            }
            else
            {
                CurrentTunnel = Instantiate(PathPrefabsCollection[PathCollectionIndex], PathSpawn.position, PathSpawn.rotation, transform);
            }

            PathSpawn = CurrentTunnel.EndTunnel;
        }

        private void AddWaypoint(Transform child, int index)
        {
            Tunnel tunnel = child.GetComponent<Tunnel>();
            CinemachinePath path = tunnel.Path;

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