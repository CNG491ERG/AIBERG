using UnityEngine;
using System;
using System.Collections.Generic;
using AIBERG.Interfaces;
using AIBERG.ScriptableObjects;

namespace AIBERG.Core{
    public class Boss : MonoBehaviour, IDamageable{
    [Header("Environment")]
    [SerializeField] private GameEnvironment environment;

    [Header("Boss Attributes")]
    [SerializeField] private int enragement;
    [SerializeField] private float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private List<Transform> droneTargetPositions = new(); 

    [Header("Boss Abilities")]
    [SerializeField] private BossAbilitiesSO bossAbilities;
    [SerializeField] private GameObject moveUpAbilityObject;
    [SerializeField] private GameObject moveDownAbilityObject;
    [SerializeField] private GameObject basicAbilityObject;
    [SerializeField] private GameObject spawnAttackDroneAbilityObject;
    public IAbility moveUpAbility;
    public IAbility moveDownAbility;
    public IAbility basicAttackAbility;
    public IAbility spawnAttackDroneAbility;
    
    [Header("IDamageable Values")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float defense;
    
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;
    
    public GameEnvironment Environment{get=>environment; private set => environment = value;}
    public float Health { get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Enragement { get => enragement; set => enragement = value;}
    public float Speed { get => speed; set => speed = value;}
    public float CooldownMultiplier { get => cooldownMultiplier; set => cooldownMultiplier = value;}
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value;}
    public List<Transform> DroneTargetPositions{get => droneTargetPositions;}
    private void Start() {
        environment = Utility.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        droneTargetPositions = Utility.ComponentFinder.FindGameObjectsWithTagInChildren("DroneTarget",this.transform).ConvertAll(obj=>obj.transform);
        if(bossAbilities.BasicAttackAbility != null){
            basicAbilityObject = Instantiate(bossAbilities.BasicAttackAbility, this.transform);
            basicAttackAbility = basicAbilityObject.GetComponent<IAbility>();
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
            spawnAttackDroneAbilityObject = Instantiate(bossAbilities.AttackDroneAbility, this.transform);
            spawnAttackDroneAbility = spawnAttackDroneAbilityObject.GetComponent<IAbility>();
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
        basicAttackAbility?.ResetCooldown();
        moveUpAbility?.ResetCooldown();
        moveDownAbility?.ResetCooldown();
        spawnAttackDroneAbility?.ResetCooldown();
    }
}
    
}
