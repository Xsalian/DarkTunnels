using DarkTunnels.Player;
using DarkTunnels.Utilities;
using MilkShake;
using UnityEngine;

namespace DarkTunnels.Gun
{
    public class GunController : MonoBehaviour, IInterctable
    {
        [field: SerializeField]
        public Transform PlayerCamera { get; set; }
        [field: SerializeField]
        public Transform CameraAnchor { get; set; }
        public bool IsControlled { get; set; }

        [field: Header("Aim settings")]
        [field: SerializeField]
        private float MouseSensitivity { get; set; }

        [field: Header("Object to rotate references")]
        [field: SerializeField]
        public Transform GunAxisX { get; set; }
        [field: SerializeField]
        public Transform GunAxisY { get; set; }

        [field: Header("Rotation clamps")]
        [field: SerializeField]
        private float MinRotationX { get; set; }
        [field: SerializeField]
        private float MaxRotationX { get; set; }
        [field: SerializeField]
        private float MinRotationY { get; set; }
        [field: SerializeField]
        private float MaxRotationY { get; set; }

        [field: Header("Shooting ray settings")]
        [field: SerializeField]
        private LayerMask IgnoreLayer { get; set; }
        [field: SerializeField]
        private float MaxShootingRayDistance { get; set; }
        [field: SerializeField]
        private Transform ShootingRayOrign { get; set; }

        [field: Header("Shooting settings")]
        [field: SerializeField]
        private float FireRate { get; set; }
        [field: SerializeField]
        private int Damage { get; set; }

        [field: Header("Shoot effect references")]
        [field: SerializeField]
        private ParticleSystem ShootParticle { get; set; }
        [field: SerializeField]
        private AudioSource ShootAudio { get; set; }
        [field: SerializeField]
        private Shaker CurrentShaker { get; set; }
        [field: SerializeField]
        private ShakePreset ShootShakePreset { get; set; }

        private float RotationX { get; set; }
        private float RotationY { get; set; }
        private float CurrentFireTimer { get; set; }
        private bool CanShoot { get; set; }

        public void Interact ()
        {
            PlayerCamera.SetParent(CameraAnchor);
            PlayerCamera.localPosition = Vector3.zero;
            PlayerCamera.localRotation = Quaternion.identity;
            IsControlled = true;
        }

        public void StopInteract ()
        {
            IsControlled = false;
        }

        protected virtual void Update ()
        {
            HandleCameraMovement();
            FireCountdown();
            TryShoot();
        }

        private void HandleCameraMovement()
        {
            if (IsControlled == true)
            {
                Vector2 aimAxis = PlayerInput.Instance.GetAimAxis();

                float mouseY = aimAxis.y * MouseSensitivity * Time.deltaTime;
                RotationX += mouseY;
                RotationX = Mathf.Clamp(RotationX, MinRotationX, MaxRotationX);
                GunAxisX.localRotation = Quaternion.Euler(RotationX, 0, 0);

                float mouseX = aimAxis.x * MouseSensitivity * Time.deltaTime;
                RotationY += mouseX;
                RotationY = Mathf.Clamp(RotationY, MinRotationY, MaxRotationY);
                GunAxisY.localRotation = Quaternion.Euler(0, RotationY, 0);
            }
        }

        private void FireCountdown ()
        {
            if (CurrentFireTimer < FireRate)
            {
                CurrentFireTimer += Time.deltaTime;
            }
            else
            {
                CanShoot = true;
            }
        }

        private void TryShoot()
        {
            if (IsControlled == true && CanShoot == true && PlayerInput.Instance.GetShootInput() == true)
            {
                CastShotRay();
                PlayShootEffect();
                CurrentFireTimer = 0;
                CanShoot = false;
            }
        }

        private void CastShotRay ()
        {
            if (Physics.Raycast(ShootingRayOrign.position, ShootingRayOrign.forward, out RaycastHit hit, MaxShootingRayDistance, ~IgnoreLayer) == true)
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeHit(Damage);
                }
            }
        }

        public void PlayShootEffect ()
        {
            ShootParticle.Play();
            ShootAudio.Play();
            CurrentShaker.Shake(ShootShakePreset);
        }
    }
}