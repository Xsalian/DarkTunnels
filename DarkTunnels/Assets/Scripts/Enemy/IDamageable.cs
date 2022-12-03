using System;

namespace DarkTunnels
{
    public interface IDamageable
    {
        public Action<BodyParts, int> OnHit { get; set; }

        public BodyParts BodyPart { get; set; }

        public void TakeHit (int damage) 
        {
            OnHit.Invoke(BodyPart, damage);
        }
    }
}