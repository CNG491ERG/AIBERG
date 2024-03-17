using System;

public interface IDamageable{
    float Health{
        get;
        set;
    }
    float Defense{
        get;
        set;
    }
    float MaxHealth{
        get;
        set;
    }
    void TakeDamage(float damageToTake);

    event EventHandler OnDamageableDeath;
    event EventHandler OnDamageableHurt;
}
