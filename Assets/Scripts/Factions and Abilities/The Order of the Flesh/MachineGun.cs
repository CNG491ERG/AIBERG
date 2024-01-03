using System.Collections;
using UnityEngine;

public class MachineGun : MonoBehaviour, IPlayerAbility, IAttackAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private float bulletsPerSecond;

    public Faction PlayerFaction => faction;

    public string AbilityName => "MachineGun";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 10f; //MUST CHANGE!

    public float Damage => 0.20f;

    public float AbilityDuration => 4.0f;

    public bool CanBeUsed => cooldownTimer >= Cooldown-0.0001f;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;

    void Start(){
        faction = GetComponentInParent<Faction>();
        cooldownTimer = Cooldown;
        bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
        AbilityLock = this;
    }

    public void UseAbility(bool inputReceived){
        if(CanBeUsed && inputReceived){
            StartCoroutine(MachineGunCoroutine(bulletsPerSecond));
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    IEnumerator MachineGunCoroutine(float bulletsPerSecond){
        faction.BasicAttack.AbilityLock = null; //get lock of basic attack
        durationTimer = 0;
        while(durationTimer < AbilityDuration){
            ShootBullet();
            durationTimer += 1f/bulletsPerSecond;
            cooldownTimer = 0;
            yield return new WaitForSeconds(1f/bulletsPerSecond);   
        }
        faction.BasicAttack.AbilityLock = faction.BasicAttack; //release lock after this ability is done
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
