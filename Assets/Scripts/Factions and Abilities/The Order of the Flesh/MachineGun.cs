using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour, IPlayerAbility, IAttackAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private float bulletsPerSecond;

    public Faction PlayerFaction => throw new System.NotImplementedException();

    public string AbilityName => "MachineGun";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 20f; //MUST CHANGE!

    public float Damage => 0.20f;

    public float AbilityDuration => 4.0f;

    public bool CanBeUsed => cooldownTimer >= Cooldown-0.0001f;

    void Start(){
        this.faction = GetComponentInParent<Faction>();
        this.cooldownTimer = this.Cooldown;
        this.durationTimer = 0;
        this.bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
    }

    public  void UseAbility(bool inputReceived){
        if(cooldownTimer >=(Cooldown-0.0001f) && inputReceived){
            StartCoroutine(MachineGunCoroutine(bulletsPerSecond, AbilityDuration));
        }
        cooldownTimer = cooldownTimer >= (Cooldown-0.001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    IEnumerator MachineGunCoroutine(float bulletsPerSecond, float duration){
        durationTimer = 0;
        while(this.durationTimer < AbilityDuration){
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab).GetComponent<Rigidbody2D>();
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(bulletVelocityX, 0);
            cooldownTimer = 0;
            this.durationTimer += 1f/bulletsPerSecond;
            yield return new WaitForSeconds(1f/bulletsPerSecond);   
        }
    }

    public void ResetCooldown(){
        cooldownTimer = 0;
    }
}
