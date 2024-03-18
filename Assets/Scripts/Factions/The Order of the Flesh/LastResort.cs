using System.Collections;
using UnityEngine;

//INCOMPLETE - NEEDS REFERENCE TO BOSS
public class LastResort : MonoBehaviour, IAttackAbility
{
    [Header("Player Reference")]
    [SerializeField] private Player player;
    
    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 17.5f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 2f;
    [SerializeField] private float durationTimer = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Projectile Used by the Ability")]
    [SerializeField] private GameObject projectilePrefab;
    //Add object pool here

    [Header("Projectile properties")]
    [SerializeField] private float projectilesPerSecond = 5; //arbitrary val
    [SerializeField] private float projectileVelocityX = 5; //arbitrary val
    [SerializeField] private float projectileDamage = 9f;

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
    #endregion
 
    void Start(){
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        ResetCooldown();
        durationTimer = 0;
        projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        projectilePrefab.GetComponent<DamagingProjectile>().tagToDamage = "Boss";
        //boss = transform.parent.parent.parent.Find("Boss").gameObject;
        AbilityLock = this;
    }

    public  void UseAbility(bool inputReceived){
        if(cooldownTimer >=(Cooldown-0.0001f) && inputReceived){
            StartCoroutine(LastResortCoroutine(projectilesPerSecond));
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    
    IEnumerator LastResortCoroutine(float misillesPerSecond){
        durationTimer = 0;
        while(durationTimer < AbilityDuration){
            ShootBullet();
            cooldownTimer = 0;
            durationTimer += 2f/misillesPerSecond;
            yield return new WaitForSeconds(2f/misillesPerSecond);
        }
    }

    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
        if(bulletRigidBody != null){
            bulletRigidBody.transform.parent = this.transform;
            //bulletRigidBody.transform.position = new Vector3(faction.player.transform.position.x - 5, boss.transform.position.y);
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(projectileVelocityX, 0);
        }
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}

