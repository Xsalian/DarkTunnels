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
        private GameObject PathPrefab { get; set; }

        [field: Space, Header("Add track input")]
        [field: SerializeField]
        private KeyCode AddTrackInput { get; set; }

        [field: Space, Header("Generator setttings")]
        [field: SerializeField]
        private int Distance { get; set; }

        private CinemachinePath.Waypoint[] WaypointsCollection { get; set; }
        private int CurrentWaypointIndex { get; set; }
        private Vector3 PathSpawnPosition { get; set; }

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
            Instantiate(PathPrefab, PathSpawnPosition, Quaternion.identity, transform);
            PathSpawnPosition += new Vector3(0, 0, 25);
        }

        private void AddWaypoint(Transform child, int index)
        {
            CinemachinePath path = child.GetComponent<CinemachinePath>();

            CinemachinePath.Waypoint wp = path.m_Waypoints[index];
            CinemachinePath.Waypoint targetWp = new CinemachinePath.Waypoint();

            targetWp.position = child.rotation * wp.position + child.localPosition;
            targetWp.roll = wp.roll;

            WaypointsCollection[CurrentWaypointIndex] = targetWp;
            CurrentWaypointIndex++;
        }
    }
}