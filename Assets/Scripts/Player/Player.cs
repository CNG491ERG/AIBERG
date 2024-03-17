using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable{
    [Header("Faction Data of the Player")]
    [SerializeField] private FactionSO faction;

    [Header("IDamageable Values")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float defense;

    public float Health{ get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [Header("Other")]
    [SerializeField] private Boss boss;
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;
    public event EventHandler OnDamageableHurtBasic;

    void Start(){
        maxHealth = health = faction.MaxHealth;
        defense = faction.Defense;
    }

    public void TakeDamage(float damageToTake){
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);
        
        if(Health == 0){
            OnDamageableDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetAllCooldowns(){
        //playerFaction.ActiveAbility1.ResetCooldown();
        //playerFaction.ActiveAbility2.ResetCooldown();
        //playerFaction.BasicAttack.ResetCooldown();
    }
}
