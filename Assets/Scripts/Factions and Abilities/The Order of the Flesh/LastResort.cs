using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class LastResort : BaseAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject misillePrefab;
    [SerializeField] private float misilleVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private float misillesPerSecond;

    [SerializeField] private GameObject boss; //RISKY LINE, TRY TO SOLVE SOME OTHER WAY!
    public override void UseAbility(bool inputReceived){
        if(cooldownTimer >=(cooldown-0.0001f) && inputReceived){
            StartCoroutine(LastResortCoroutine(misillesPerSecond, duration));
        }
        cooldownTimer = cooldownTimer >= (cooldown-0.001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.faction = GetComponentInParent<Faction>();
        this.abilityName = "LastResort";
        this.abilityOwner = faction.player.gameObject;
        this.abilityType = AbilityType.ACTIVE2;
        this.cooldown = 0.20f; //CHANGE IT!!
        this.cooldownTimer = this.cooldown;
        this.duration = 2.0f;
        this.durationTimer = 0;
        this.damage = 10f;
        this.misillePrefab.GetComponent<DamagingProjectile>().damage = this.damage;
        this.boss = FindFirstObjectByType<Boss>().gameObject;
    }

    IEnumerator LastResortCoroutine(float misillesPerSecond, float duration){
        durationTimer = 0;
        while(this.durationTimer < this.duration){
            Rigidbody2D bulletRigidBody = Instantiate(misillePrefab).GetComponent<Rigidbody2D>();
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.position = new Vector3(faction.player.transform.position.x - 5, boss.transform.position.y);
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(misilleVelocityX, 0);
            cooldownTimer = 0;
            this.durationTimer += 1f/misillesPerSecond;
            yield return new WaitForSeconds(1f/misillesPerSecond);
        }
    }
}

