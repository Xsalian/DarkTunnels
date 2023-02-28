using UnityEngine;
using DarkTunnels.Utilities;

namespace DarkTunnels.Player
{
    public class PlayerInput : SingletonMonoBehaviour<PlayerInput>
    {
        private Controls PlayerControls { get; set; }

        public bool GetInteractInput ()
        {
            return PlayerControls.Player.Interact.IsPressed(); 
        }

        public bool GetStopInteractInput ()
        {
            return PlayerControls.Player.StopInteract.IsPressed();
        }

        public Vector2 GetAimAxis ()
        {
            return PlayerControls.Player.AimAxis.ReadValue<Vector2>();
        }

        public bool GetShootInput ()
        {
            return PlayerControls.Player.Shoot.IsPressed();
        }

        protected override void Awake ()
        {
            base.Awake();
            InitializePlayerControls();
        }

        private void InitializePlayerControls ()
        {
            PlayerControls = new Controls();
            PlayerControls.Player.Enable();
        }
    }
}