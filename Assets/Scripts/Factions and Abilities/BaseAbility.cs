using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType{
        BASIC,
        ACTIVE,
        PASSIVE,
        JUMP
    }
public abstract class BaseAbility : MonoBehaviour, IAbility{
    public GameObject abilityOwner;
    public float cooldown;
    public float duration;
    public float damage;
    public string abilityName;
    public AbilityType abilityType;
    public abstract void UseAbility(bool inputReceived);
}
