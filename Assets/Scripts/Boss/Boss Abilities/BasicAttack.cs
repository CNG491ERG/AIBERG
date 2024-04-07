using UnityEngine;

public class BasicAttack : MonoBehaviour, IAttackAbility{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;
    
    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 0.50f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Projectile properties")]
    [SerializeField] private GameObject projectilePrefab;
    //Add object pool here
    [SerializeField] private float projectileVelocityX;
    [SerializeField] private float projectileDamage = 7.5f;
    [SerializeField] private string tagToDamage = "Player";

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
        projectilePrefab.GetComponent<DamagingProjectile>().damage = projectileDamage;
        projectilePrefab.GetComponent<DamagingProjectile>().AddTagToDamage(tagToDamage);
        AbilityLock = this;
    }

    private void FixedUpdate() {
        canBeUsed = cooldownTimer >= (Cooldown-0.001f);
        cooldownTimer = canBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public void UseAbility(bool inputReceived){
        if(canBeUsed && inputReceived){
            Rigidbody2D projectileRb = Instantiate(projectilePrefab, boss.Environment.transform).GetComponent<Rigidbody2D>();
            boss.Environment.AddObjectToEnvironmentList(projectileRb.gameObject);
            if(projectileRb != null){
                projectileRb.transform.position = boss.transform.position;
                projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(-projectileVelocityX, 0);
            }
            cooldownTimer = 0;
        }
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }

}
