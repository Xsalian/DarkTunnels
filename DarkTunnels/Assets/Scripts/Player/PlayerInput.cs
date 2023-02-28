using UnityEngine;

namespace DarkTunnels.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput Instance { get; private set; }
        private Controls PlayerControls { get; set; }

        public bool GetInteractInput ()
        {
            return PlayerControls.Player.Interact.IsPressed(); 
        }

        public bool GetStopInteractInput ()
        {
            return PlayerControls.Player.StopInteract.IsPressed();
        }

        protected virtual void Awake ()
        {
            CreateSingleton();
            InitializePlayerControls();
        }

        private void CreateSingleton ()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        private void InitializePlayerControls ()
        {
            PlayerControls = new Controls();
            PlayerControls.Player.Enable();
        }
    }
}