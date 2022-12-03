using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace DarkTunnels
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

        private List<EnemyController> EnemyCollection { get; set; } = new();

        protected virtual void Update()
        {
            SetSpawnPosition();
            HandleInput();
        }

        private void SetSpawnPosition()
        {
            CurrentDollyCart.m_Position = TrainLastCart.m_Position - SpaceDistance;
        }

        private void HandleInput()
        {
            //try new input system, DEBUG INPUT REMOVE LATER
            if (Input.GetKeyDown(KeyCode.T))
            {
                SpawnEnemy();
            }

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
            return CurrentEnemiesOnRoute.EnemyTypeCollection[Random.Range(0, CurrentEnemiesOnRoute.EnemyTypeCollection.Length)];
        }
    }
}