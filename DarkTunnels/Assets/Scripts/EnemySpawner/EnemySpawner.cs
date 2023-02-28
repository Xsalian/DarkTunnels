using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkTunnels.Enemy
{
	public class EnemySpawner : MonoBehaviour
    {
        [field: Header("References for enemy")]
        [field: SerializeField]
        private Transform SpawnPointTransform { get; set; }
        [field: SerializeField]
        private Transform TrainTransform { get; set; }

        [field: Header("Spawner settings")]
        [field: SerializeField]
        private EnemiesOnRoute CurrentEnemiesOnRoute { get; set; }
        [field: SerializeField]
        private CinemachineDollyCart TrainLastCart { get; set; }
        [field: SerializeField]
        private CinemachineDollyCart CurrentDollyCart { get; set; }
        [field: SerializeField]
        private float SpaceDistance { get; set; }

        [field: Space, Header("Spawn frequency settings")]
        [field: SerializeField]
        public float MaxSpawnDelay { get; private set; }
        [field: SerializeField]
        public float MinSpawnDelay { get; private set; }
        [field: SerializeField]
        public float TimeToReachMinValue { get; private set; }

        private List<EnemyController> EnemyCollection { get; set; } = new();
        private bool CanSpawn { get; set; } = true;
        private float TimerSpawn { get; set; }

        protected virtual void Update()
        {
            SetSpawnPosition();
            TrySpawnEnemy();
            HandleInput();
        }

        private void SetSpawnPosition()
        {
            CurrentDollyCart.m_Position = TrainLastCart.m_Position - SpaceDistance;
        }

        private void TrySpawnEnemy ()
        {
            if (TimerSpawn <= TimeToReachMinValue)
            {
                TimerSpawn += Time.deltaTime;
            }

            if (CanSpawn == true)
            {
                SpawnEnemy();
                float timeToWait = Mathf.Lerp(MaxSpawnDelay, MinSpawnDelay, TimerSpawn / TimeToReachMinValue);
                StartCoroutine(WaitForNextSpawn(timeToWait));
            }
        }

        private void HandleInput()
        {
            //try new input system, DEBUG INPUT REMOVE LATER
            if (Input.GetKeyDown(KeyCode.O))
            {
                DestroyAllEnemies();
            }
        }

        private void SpawnEnemy()
        {
            EnemyController enemy = Instantiate(DrawRandomEnemy(), SpawnPointTransform.position, Quaternion.identity);
            enemy.LastTrainCart = TrainTransform;
            EnemyCollection.Add(enemy);
        }

        private void DestroyAllEnemies()
        {
            foreach (EnemyController enemy in EnemyCollection)
            {
                Destroy(enemy.gameObject);
            }

            EnemyCollection.Clear();
        }

        private EnemyController DrawRandomEnemy()
        {
            return CurrentEnemiesOnRoute.EnemyTypeCollection[UnityEngine.Random.Range(0, CurrentEnemiesOnRoute.EnemyTypeCollection.Length)];
        }

        private IEnumerator WaitForNextSpawn(float time)
        {
            CanSpawn = false;
            yield return new WaitForSeconds(time);
            CanSpawn = true;
        }
    }
}