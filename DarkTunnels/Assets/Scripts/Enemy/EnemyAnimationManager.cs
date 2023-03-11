using System;
using UnityEngine;

namespace DarkTunnels
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        public Action OnAttack = delegate { };

        public void Attack ()
        {
            OnAttack.Invoke();
        }
    }
}