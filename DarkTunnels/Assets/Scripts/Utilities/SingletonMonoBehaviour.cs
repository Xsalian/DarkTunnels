using UnityEngine;

namespace DarkTunnels.Utilities
{
    [DefaultExecutionOrder(-1000)]
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake ()
        {
            InitializeSingleton();
        }

        protected virtual void OnDestroy ()
        {
            TrySetInstanceToNull();
        }

        private void InitializeSingleton ()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }

        private void TrySetInstanceToNull ()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
