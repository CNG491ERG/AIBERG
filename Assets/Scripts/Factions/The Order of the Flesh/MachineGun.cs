using System.Collections;
using UnityEngine;

public class MachineGun : MonoBehaviour, IAttackAbility{
    [SerializeField] private Player player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private float bulletsPerSecond;
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

    public GameObject AbilityOwner => player.gameObject;

    private IAbility abilityLock;

    void Start(){
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        cooldownTimer = Cooldown;
        bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
        AbilityLock = (IAbility)this;
    }

    public void UseAbility(bool inputReceived){
        if(CanBeUsed && inputReceived){
            StartCoroutine(MachineGunCoroutine(bulletsPerSecond));
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    IEnumerator MachineGunCoroutine(float bulletsPerSecond){
        //GET LOCK
        durationTimer = 0;
        while(durationTimer < AbilityDuration){
            ShootBullet();
            durationTimer += 1f/bulletsPerSecond;
            cooldownTimer = 0;
            yield return new WaitForSeconds(1f/bulletsPerSecond);   
        }
        //RELEASE LOCK
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
