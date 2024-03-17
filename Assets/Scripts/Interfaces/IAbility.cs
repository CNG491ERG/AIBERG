using UnityEngine;

//This is common for all abilites, do not directly inherit this.
public interface IAbility{
    public GameObject AbilityOwner{
        get;
    }
    public float Cooldown{
        get;
    }
    public bool CanBeUsed{
        get;
    }
    public float AbilityDuration{
        get;
    }

    public IAbility AbilityLock{
        get;
        set;
    }
    
    public void UseAbility(bool inputReceived);
    public void ResetCooldown();
}
