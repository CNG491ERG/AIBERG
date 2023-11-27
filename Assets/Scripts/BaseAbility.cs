using System.Collections;

public abstract class BaseAbility : IAbility{
    protected float cooldown;
    protected float duration;
    protected float damage;
    protected float abilityName;
    protected float abilityID;

    public abstract IEnumerator UseAbility();
}
