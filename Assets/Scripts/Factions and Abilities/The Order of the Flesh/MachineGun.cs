using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : BaseAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private int bulletsPerSecond;


    void Start(){
        this.faction = GetComponentInParent<Faction>();
        this.abilityName = "MachineGun";
        this.abilityOwner = faction.player.gameObject;
        this.abilityType = AbilityType.ACTIVE1;
        this.cooldown = 0.20f; //CHANGE IT!!
        this.cooldownTimer = this.cooldown;
        this.duration = 4.0f;
        this.durationTimer = 0;
        this.damage = 0.20f;
        this.bulletPrefab.GetComponent<DamagingProjectile>().damage = this.damage;
    }

    public override void UseAbility(bool inputReceived){
        if(cooldownTimer >=(cooldown-0.0001f) && inputReceived){
            StartCoroutine(MachineGunCoroutine(bulletsPerSecond, duration));
        }
        cooldownTimer = cooldownTimer >= (cooldown-0.001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    IEnumerator MachineGunCoroutine(int bulletsPerSecond, float duration){
        durationTimer = 0;
        while(this.durationTimer < this.duration){
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab).GetComponent<Rigidbody2D>();
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(bulletVelocityX, 0);
            cooldownTimer = 0;
            this.durationTimer += (float)1/bulletsPerSecond;
            yield return new WaitForSeconds((float)1/bulletsPerSecond);
            
        }
    }
}
