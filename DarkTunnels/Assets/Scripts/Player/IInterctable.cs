using UnityEngine;

namespace DarkTunnels
{
    public interface IInterctable
    {
        public Transform PlayerCamera { get; set; }
        public Transform CameraAnchor { get; set; }
        public bool IsControlled { get; set; }
        public void Interact();
        public void StopInteract();
    }
}