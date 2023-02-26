using UnityEngine;

namespace DarkTunnels
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        [field: SerializeField]
        private EnemyAudioController AudioController { get; set; }

        public void Attack ()
        {
            AudioController.PlaySFX(EnemyAudioType.ATTACK);
        }
    }
}