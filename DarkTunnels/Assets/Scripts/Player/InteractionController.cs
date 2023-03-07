using UnityEngine;

namespace DarkTunnels.Player
{
    public class InteractionController : MonoBehaviour
    {
        [field: SerializeField]
        public Transform PlayerCamera { get; set; }
        [field: SerializeField]
        public Transform CameraPlayerAnchor { get; set; }
        [field: SerializeField]
        private FirstPersonMovement MovementController { get; set; }
        [field: SerializeField]
        private FirstPersonLook LookController { get; set; }
        [field: SerializeField]
        private Transform RayOrigin { get; set; }
        [field: SerializeField]
        private float RayMaxDistance { get; set; }

        private IInterctable CurrentInterctable { get; set; }

        protected virtual void Update ()
        {
            CastInteractionRay();
            TryExitInteraction();
        }

        private void CastInteractionRay ()
        {
            if (PlayerInput.Instance.GetInteractInput() == true)
            {
                if (Physics.Raycast(RayOrigin.position, RayOrigin.forward, out RaycastHit hit, RayMaxDistance) == true)
                {
                    IInterctable interctable = hit.collider.GetComponent<IInterctable>();

                    if (interctable != null)
                    {
                        CurrentInterctable = interctable;
                        SetFirstPersonControllerEnableState(false);
                        CurrentInterctable.Interact();
                    }
                }
            }
        }

        private void TryExitInteraction ()
        {
            if (CurrentInterctable != null && PlayerInput.Instance.GetStopInteractInput() == true)
            {
                CurrentInterctable.StopInteract();
                SetFirstPersonControllerEnableState(true);
                SetCameraLocation();
            }
        }

        private void SetFirstPersonControllerEnableState (bool state)
        {
            MovementController.enabled = state;
            LookController.enabled = state;
        }

        private void SetCameraLocation ()
        {
            PlayerCamera.SetParent(CameraPlayerAnchor);
            PlayerCamera.localPosition = Vector3.zero;
            PlayerCamera.localRotation = Quaternion.identity;
        }
    }
}