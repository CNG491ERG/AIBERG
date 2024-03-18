using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IAttackAbility{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;
    
    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 0.50f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Projectile Used by the Ability")]
    [SerializeField] private GameObject projectilePrefab;
    //Add object pool here

    [Header("Projectile properties")]
    [SerializeField] private float projectileVelocityX;
    [SerializeField] private float projectileDamage = 7.5f;

    #region interface properties
    public float Cooldown => abilityCooldown;
    public float AbilityDuration => abilityDuration;
    public float Damage => projectileDamage;
    public bool CanBeUsed => canBeUsed;
    private IAbility abilityLock;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    public GameObject AbilityOwner => boss.gameObject;
    #endregion
 
    private void Start() {
        boss = Utility.ComponentFinder.FindComponentInParents<Boss>(this.transform);
        projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        projectilePrefab.GetComponent<DamagingProjectile>().tagToDamage = "Player";
        AbilityLock = this;
    }

    private void FixedUpdate() {
        canBeUsed = cooldownTimer >= (Cooldown-0.001f);
        cooldownTimer = canBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public void UseAbility(bool inputReceived){
        if(canBeUsed && inputReceived){
            Rigidbody2D projectileRb = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
            if(projectileRb != null){
                projectileRb.transform.parent = this.transform;
                projectileRb.transform.localPosition = Vector3.zero;
                projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(-projectileVelocityX, 0);
            }
            cooldownTimer = 0;
            Debug.Log("Projectile velocity: " + projectileRb.velocity);
        }
        cooldownTimer = cooldownTimer >= (Cooldown-0.0001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }

}
