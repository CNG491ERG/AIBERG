using System;

namespace AIBERG.Interfaces
{
    public interface IDamageable
    {
        float Health
        {
            get;
            set;
        }
        float Defense
        {
            get;
            set;
        }
        float MaxHealth
        {
            get;
            set;
        }
        void TakeDamage(float damageToTake);

        event EventHandler OnDamageableDeath;
        event EventHandler<DamageEventArgs> OnDamageableHurt;
        public class DamageEventArgs : EventArgs
        {
            public float Damage { get; }
            public DamageEventArgs(float damage)
            {
                Damage = damage;
            }
        }
    }

}
