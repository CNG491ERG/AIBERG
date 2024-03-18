using UnityEngine;
using System;

public class Boss : MonoBehaviour, IDamageable{
    [Header("IDamageable Values")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float defense;

    [Header("Boss Attributes")]
    [SerializeField] private int enragement;
    [SerializeField] private float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;

    [Header("Boss Abilities")]
    [SerializeField] private BossAbilitiesSO bossAbilities;
    [SerializeField] private GameObject moveUpAbilityObject;
    [SerializeField] private GameObject moveDownAbilityObject;
    [SerializeField] private GameObject basicAbilityObject;
    [SerializeField] private GameObject attackDroneAbilityObject;
    public IAbility moveUpAbility;
    public IAbility moveDownAbility;
    public IAbility basicAbility;
    public IAbility attackDroneAbility;

    [Header("Other")]
    public Player player;
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;

    public float Health { get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Start() {
        if(bossAbilities.BasicAbility != null){
            basicAbilityObject = Instantiate(bossAbilities.BasicAbility, this.transform);
            basicAbility = basicAbilityObject.GetComponent<IAbility>();
        }
        if(bossAbilities.MoveUpAbility != null){
            moveUpAbilityObject = Instantiate(bossAbilities.MoveUpAbility, this.transform);
            moveUpAbility = moveUpAbilityObject.GetComponent<IAbility>();
        }
        if(bossAbilities.MoveDownAbility != null){
            moveDownAbilityObject = Instantiate(bossAbilities.MoveDownAbility, this.transform);
            moveDownAbility = moveDownAbilityObject.GetComponent<IAbility>();
        }
        if(bossAbilities.AttackDroneAbility != null){
            attackDroneAbilityObject = Instantiate(bossAbilities.AttackDroneAbility, this.transform);
            attackDroneAbility = attackDroneAbilityObject.GetComponent<IAbility>();
        }

        health = maxHealth = bossAbilities.MaxHealth;
        defense = bossAbilities.Defense;
        speed = bossAbilities.Speed;
    }
    
    public void TakeDamage(float damageToTake) {
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);

        if(Health == 0){
            OnDamageableDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetAllCooldowns(){
        basicAbility?.ResetCooldown();
        moveUpAbility?.ResetCooldown();
        moveDownAbility?.ResetCooldown();
        attackDroneAbility?.ResetCooldown();
    }
}
    