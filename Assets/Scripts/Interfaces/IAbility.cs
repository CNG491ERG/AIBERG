using UnityEngine;

//This is common for all abilites, do not directly inherit this.
public interface IAbility{
    public string AbilityName{
        get;
    }
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
    public void UseAbility(bool inputReceived);
}
