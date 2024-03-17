using UnityEngine;

public class AssaultRifle : MonoBehaviour, IAttackAbility{
    [SerializeField] private Player player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer;
    public float Cooldown => 0.30f;
    public float AbilityDuration => 0;
    public float Damage => 0.20f;
    public bool CanBeUsed => cooldownTimer >= (Cooldown-0.001f);
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

    private void Start() {
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        cooldownTimer = Cooldown;
        bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
        AbilityLock = (IAbility)this;
    }

    public void UseAbility(bool inputReceived){
        if(cooldownTimer>=(Cooldown-0.0001f) && inputReceived && AbilityLock != null){ 
            ShootBullet();
            cooldownTimer = 0;
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab).GetComponent<Rigidbody2D>();
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
