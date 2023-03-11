using DarkTunnels.Utilities;
using System;
using UnityEngine;

namespace DarkTunnels.Train
{
    public class TrainController : SingletonMonoBehaviour<TrainController>
    {
        public Action OnTrainDestoryed = delegate { };
        public Action OnStationReached = delegate { };

        [field: SerializeField]
        private int MaxHealth { get; set; }
        [field: SerializeField]
        private LayerMask EndTrackPointLayer { get; set; }

        private int CurrentHealth { get; set; }

        public void TakeDamage (int damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                OnTrainDestoryed.Invoke();
            }
        }

        protected override void Awake ()
        {
            base.Awake();
            InitializeStatistic();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (EndTrackPointLayer == (EndTrackPointLayer | (1 << other.gameObject.layer)))
            {
                OnStationReached.Invoke();
            }
        }

        private void InitializeStatistic ()
        {
            CurrentHealth = MaxHealth;
        }
    }
}