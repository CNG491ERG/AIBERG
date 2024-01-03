using UnityEngine;

public class AssaultRifle : MonoBehaviour, IPlayerAbility, IAttackAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer;

    public Faction PlayerFaction => faction;

    public string AbilityName => "AssaultRifle";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 0.20f;

    public float AbilityDuration => 0;

    public float Damage => 0.25f;

    public bool CanBeUsed => cooldownTimer >= (Cooldown-0.001f);
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;

    private void Start() {
        faction = GetComponentInParent<Faction>();
        cooldownTimer = Cooldown;
        bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
        AbilityLock = this;
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
        bulletRigidBody.transform.parent = this.transform;
        bulletRigidBody.transform.localPosition = Vector3.zero;
        bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(bulletVelocityX, 0);
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}
