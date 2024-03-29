using DarkTunnels.Train;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DarkTunnels
{
    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField]
        private EnemyStatisticsData EnemyStatistics { get; set; }
        [field: SerializeField]
        protected GameObject AliveEnemy { get; set; }
        [field: SerializeField]
        private NavMeshAgent EnemyAgent { get; set; }
        [field: SerializeField]
        private List<EnemyBodyPart> BodyPartCollection { get; set; }
        [field: SerializeField]
        private EnemyAnimationManager AnimationManager { get; set; }
        [field: SerializeField]
        private Animator AnimationController { get; set; }
        [field: SerializeField]
        private EnemyAudioController AudioController { get; set; }

        [field: Header("Dead body variants")]
        [field: SerializeField]
        private GameObject HeadShatter { get; set; }
        [field: SerializeField]
        private GameObject StomachShatter { get; set; }

        public Transform LastTrainCart { get; set; }
        protected int CurrentHealthPoints { get; set; }

        private const string ATTACK_ANIMATION_NAME = "Attack";
        private const string RUN_ANIMATION_NAME = "Run";

        protected virtual void Awake ()
        {
            Initialize();
        }

        protected virtual void OnEnable ()
		{
            AttachToEvents();
		}

        protected virtual void OnDisable ()
		{
            DetachFromEvents();
		}

        protected virtual void Update ()
        {
            SetEnemyDestination();
        }

        private void Initialize ()
        {
            AudioController.PlaySFX(EnemyAudioType.IDLE);
            InitializeStatistics();
        }

        private void InitializeStatistics ()
        {
            CurrentHealthPoints = EnemyStatistics.MaxHealthPoints;
            EnemyAgent.speed = EnemyStatistics.Speed;
        }

        private void AttachToEvents ()
		{
            for (int index = 0; index < BodyPartCollection.Count; index++)
			{
                BodyPartCollection[index].OnHit += HandleOnHit;

                if (BodyPartCollection[index].IsSetToDetectCollisonWithTrain == true)
				{
                    BodyPartCollection[index].OnCollisonWithTrain += HandleOnCollisonWithTrain;
                }
			}

            AnimationManager.OnAttack += HandleOnAttackEvent;
        }

        private void DetachFromEvents ()
        {
            for (int index = 0; index < BodyPartCollection.Count; index++)
            {
                BodyPartCollection[index].OnHit -= HandleOnHit;

                if (BodyPartCollection[index].IsSetToDetectCollisonWithTrain == true)
                {
                    BodyPartCollection[index].OnCollisonWithTrain -= HandleOnCollisonWithTrain;
                }
            }

            AnimationManager.OnAttack -= HandleOnAttackEvent;
        }

        private void SetEnemyDestination ()
		{
            if (EnemyAgent.enabled == true)
            {
                EnemyAgent.SetDestination(LastTrainCart.position);
            }
        }

        private void HandleOnHit (BodyParts bodyPart, int damage)
        {
            switch (bodyPart)
            {
                case BodyParts.HEAD:
                    CurrentHealthPoints -= damage;

                    if (CurrentHealthPoints <= 0)
                    {
                        DisplayCorpse(HeadShatter);
                    }

                    break;
                case BodyParts.STOMACH:
                    CurrentHealthPoints -= damage;

                    if (CurrentHealthPoints <= 0)
                    {
                        DisplayCorpse(StomachShatter);
                    }

                    break;
                default:
                    Debug.LogWarning("MISSING BODY PART");
                    break;
            }
        }

        private void DisplayCorpse (GameObject corpse)
        {
            DetachFromEvents();

            EnemyAgent.enabled = false; 

            AliveEnemy.SetActive(false);
            corpse.SetActive(true);
            
            AudioController.PlaySFX(EnemyAudioType.DEATH);
        }

        private void HandleOnCollisonWithTrain (bool isEnter)
		{
			if (isEnter)
			{
                AnimationController.Play(ATTACK_ANIMATION_NAME);
            }
			else
			{
                AnimationController.Play(RUN_ANIMATION_NAME);
                AudioController.PlaySFX(EnemyAudioType.IDLE);
            }
		}

        private void HandleOnAttackEvent ()
        {
            AudioController.PlaySFX(EnemyAudioType.ATTACK);
            TrainController.Instance.TakeDamage(EnemyStatistics.AttackPower);
        }
    }
}
