using System;
using AIBERG.InputHandlers;
using AIBERG.Interfaces;
using AIBERG.ScriptableObjects;
using UnityEngine;

namespace AIBERG.Core{
public class Player : MonoBehaviour, IDamageable{
    [Header("Player's Input Handler")]
    public InputHandler inputHandler;

    [Header("Environment")]
    [SerializeField] private GameEnvironment environment;

    [Header("Faction Data of the Player")]
    [SerializeField] private FactionSO faction;
    [SerializeField] private GameObject basicAbilityObject;
    [SerializeField] private GameObject activeAbility1Object;
    [SerializeField] private GameObject activeAbility2Object;
    [SerializeField] private GameObject passiveAbilityObject;
    [SerializeField] private GameObject jumpObject;
    public IAbility basicAbility;
    public IAbility activeAbility1;
    public IAbility activeAbility2;
    public IAbility passiveAbility;
    public IAbility jump;

    [Header("IDamageable Values")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float defense;

    public GameEnvironment Environment{get=>environment; private set => environment = value;}
    public float Health{ get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    

    [Header("Other")]
    [SerializeField] private Boss boss;
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;

    void Start(){
        environment = Utility.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        inputHandler = GetComponent<InputHandler>();
        if(faction.BasicAbility != null){
            basicAbilityObject = Instantiate(faction.BasicAbility, this.transform);
            basicAbility = basicAbilityObject.GetComponent<IAbility>();
        }
        if(faction.ActiveAbility1 != null){
            activeAbility1Object = Instantiate(faction.ActiveAbility1, this.transform);
            activeAbility1 = activeAbility1Object.GetComponent<IAbility>();
        }
        if(faction.ActiveAbility2 != null){
            activeAbility2Object = Instantiate(faction.ActiveAbility2, this.transform);
            activeAbility2 = activeAbility2Object.GetComponent<IAbility>();
        }
        if(faction.PassiveAbility != null){
            passiveAbilityObject = Instantiate(faction.PassiveAbility, this.transform);
            passiveAbility = passiveAbilityObject.GetComponent<IAbility>();
        }
        if(faction.Jump != null){
            jumpObject = Instantiate(faction.Jump, this.transform);
            jump = jumpObject.GetComponent<IAbility>();
        }

        health = maxHealth = faction.MaxHealth;
        defense = faction.Defense;
    }

    private void FixedUpdate() {
        if(this.GetComponent<PlayerAgent>() == null){
            basicAbility?.UseAbility(inputHandler.BasicAbilityInput);
            activeAbility1?.UseAbility(inputHandler.ActiveAbility1Input);
            activeAbility2?.UseAbility(inputHandler.ActiveAbility2Input);
            jump?.UseAbility(inputHandler.JumpInput);
        }   
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
        basicAbility?.ResetCooldown();
        activeAbility1?.ResetCooldown();
        activeAbility2?.ResetCooldown();
        jump?.ResetCooldown();
        passiveAbility?.ResetCooldown();
    }
}

}
