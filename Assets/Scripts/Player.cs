using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable{

    private float health;
    private float defense;
    private Faction playerFaction;
    private float maxHealth = 100;
    [SerializeField] public Boss boss;

    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;


    // Start is called before the first frame update
    void Start(){
        playerFaction = GetComponentInChildren<Faction>();
        health = playerFaction.health;
        defense = playerFaction.defense;
    }

     public float Health{
        get{
            return health;
        }
        set{
            health = value;
        }
    }

    public float Defense {
        get{
            return defense;
        }
        set{
            defense = value;
        }
    }

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void TakeDamage(float damageToTake){
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);
        if(Health == 0){
            OnDamageableDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetAllCooldowns(){
        playerFaction.ActiveAbility1.ResetCooldown();
        playerFaction.ActiveAbility2.ResetCooldown();
        playerFaction.BasicAttack.ResetCooldown();
    }
}
