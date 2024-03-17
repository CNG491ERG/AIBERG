using UnityEngine;

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
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float bulletDamage = 0.20f;

    #region interface properties
    public float Cooldown => abilityCooldown;
    public float AbilityDuration => abilityDuration;
    public float Damage => bulletDamage;
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
    #endregion

    private void Start() {
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        ResetCooldown();
        projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        AbilityLock = this;
    }

    private void FixedUpdate() {
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
        Rigidbody2D bulletRigidBody = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
        if(bulletRigidBody != null){
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(bulletVelocityX, 0);
        }
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}
