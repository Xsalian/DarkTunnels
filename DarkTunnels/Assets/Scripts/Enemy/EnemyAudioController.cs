using UnityEngine;

namespace DarkTunnels
{
    public class EnemyAudioController : MonoBehaviour
    {
        [field: SerializeField]
        public AudioSource Source { get; set; }

        [field: Header("Audio Clips")]
        [field: SerializeField]
        public AudioClip Idle { get; set; }
        [field: SerializeField]
        public AudioClip Attack { get; set; }
        [field: SerializeField]
        public AudioClip Death { get; set; }

        public void PlaySFX (EnemyAudioType audioType)
        {
            switch (audioType)
            {
                case EnemyAudioType.IDLE:
                    Source.clip = Idle;
                    Source.loop = true;
                    Source.Play();
                    break;
                case EnemyAudioType.ATTACK:
                    Source.clip = Attack;
                    Source.loop = false;
                    Source.Play();
                    break;
                case EnemyAudioType.DEATH:
                    Source.clip = Death;
                    Source.loop = false;
                    Source.Play();
                    break;
            }
        }
    }
}