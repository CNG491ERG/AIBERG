using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamageable{
    float Health{
        get;
        set;
    }
    float Defense{
        get;
        set;
    }
    void TakeDamage(float damageToTake);
}
