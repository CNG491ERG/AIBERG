using UnityEngine;
using AIBERG.Interfaces;
using AIBERG.Core;

namespace AIBERG.Factions.TheOrderOfTheFlesh{
public class AssaultRifle : MonoBehaviour, IAttackAbility{
    [Header("Player Reference")]
    [SerializeField] private Player player;
    
    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 0.30f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Projectile Used by the Ability")]
    [SerializeField] private GameObject projectilePrefab;
    //Add object pool here

    [Header("Projectile properties")]
    [SerializeField] private float projectileVelocityX;
    [SerializeField] private float projectileDamage = 0.20f;

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
    public GameObject AbilityOwner => player.gameObject;

    public bool ownsLock;
    #endregion

    private void Start() {
        player = Utilities.ComponentFinder.FindComponentInParents<Player>(this.transform);
        ResetCooldown();
        projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        projectilePrefab.GetComponent<DamagingProjectile>().AddTagToDamage("Boss");
        projectilePrefab.GetComponent<DamagingProjectile>().AddTagToDamage("AttackDrone");
        AbilityLock = this;
    }

    private void FixedUpdate() {
        ownsLock = AbilityLock == (IAbility)this;
        canBeUsed = cooldownTimer >= (Cooldown-0.001f);
        cooldownTimer = canBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public void UseAbility(bool inputReceived){
        if(canBeUsed && inputReceived && AbilityLock != null){ 
            ShootBullet();
            cooldownTimer = 0;
        }
    }

    private void ShootBullet(){
        Rigidbody2D projectileRb = Instantiate(projectilePrefab, player.Environment.transform).GetComponent<Rigidbody2D>();
        player.Environment.AddObjectToEnvironmentList(projectileRb.gameObject);
        if(projectileRb != null){
            projectileRb.transform.position = player.transform.position;
            projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(projectileVelocityX, 0);
        }
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}

}
