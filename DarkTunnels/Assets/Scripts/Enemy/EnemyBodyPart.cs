using System;
using UnityEngine;

namespace DarkTunnels
{
	public class EnemyBodyPart : MonoBehaviour, IDamageable
	{
		public Action<BodyParts, int> OnHit { get; set; }
		public Action<bool> OnCollisonWithTrain { get; set; }

		[field: SerializeField]
		public BodyParts BodyPart { get; set; }

		[field: SerializeField]
		public bool IsSetToDetectCollisonWithTrain { get; set; }

		protected virtual void OnTriggerEnter (Collider other)
		{
			if (other.CompareTag("EnemyDestinationPoint") == true && IsSetToDetectCollisonWithTrain == true) //MAGIC NUMBER PATTERN, STINKY TAG TO CONST, MOVE LOGIC TO SEPARATE FUNCTION
			{
				OnCollisonWithTrain.Invoke(true);
			}
		}

		protected virtual void OnTriggerExit (Collider other)
		{
			if (other.CompareTag("EnemyDestinationPoint") == true && IsSetToDetectCollisonWithTrain == true) //MAGIC NUMBER PATTERN, STINKY TAG TO CONST
			{
				OnCollisonWithTrain.Invoke(false);
			}
		}
	}
}