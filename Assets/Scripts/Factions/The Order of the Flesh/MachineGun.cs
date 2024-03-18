using System.Collections;
using UnityEngine;

public class MachineGun : MonoBehaviour, IAttackAbility{
    [Header("Player Reference")]
    [SerializeField] private Player player;
    
    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 10f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 4.0f;
    [SerializeField] private float durationTimer = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Projectile Used by the Ability")]
    [SerializeField] private GameObject projectilePrefab;
    //Add object pool here

    [Header("Projectile properties")]
    [SerializeField] private float projectilesPerSecond = 5; //arbitrary val
    [SerializeField] private float projectileVelocityX = 5; //arbitrary val
    [SerializeField] private float projectileDamage = 0.20f;
    
    #region interface properties
    public float Cooldown => abilityCooldown;
    public float Damage => projectileDamage;
    public float AbilityDuration => abilityDuration;
    public bool CanBeUsed => canBeUsed;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    public GameObject AbilityOwner => player.gameObject;
    private IAbility abilityLock;
    #endregion

    void Start(){
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        ResetCooldown();
        projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        projectilePrefab.GetComponent<DamagingProjectile>().tagToDamage = "Boss";
        AbilityLock = this;
    }

    private void FixedUpdate() {
        canBeUsed = cooldownTimer >= (Cooldown-0.001f);
        cooldownTimer = canBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    
    public void UseAbility(bool inputReceived){
        if(canBeUsed && inputReceived){
            StartCoroutine(MachineGunCoroutine(projectilesPerSecond));
        }
    }
    IEnumerator MachineGunCoroutine(float projectilesPerSecond){
        player.basicAbility.AbilityLock = null;
        durationTimer = 0;
        while(durationTimer < abilityDuration){
            ShootBullet();
            durationTimer += 1f/projectilesPerSecond;
            cooldownTimer = 0;
            yield return new WaitForSeconds(1f/projectilesPerSecond);   
        }
        player.basicAbility.AbilityLock = player.basicAbility;
    }
    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
        if(bulletRigidBody != null){
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(projectileVelocityX, 0);
        }
    }
    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}