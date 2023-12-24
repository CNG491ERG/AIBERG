using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class LastResort : MonoBehaviour, IPlayerAbility, IAttackAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject misillePrefab;
    [SerializeField] private float misilleVelocityX;
    [SerializeField] private float cooldownTimer; 
    [SerializeField] private float durationTimer;
    [SerializeField] private float misillesPerSecond;

    [SerializeField] private GameObject boss; //RISKY LINE, TRY TO SOLVE SOME OTHER WAY!
    public Faction PlayerFaction => faction;

    public string AbilityName => "LastResort";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 15f; //Change it

    public float AbilityDuration => 2.0f;
    public float Damage => 10f;

    public bool CanBeUsed => cooldownTimer >= Cooldown-0.0001f;

    void Start(){
        faction = GetComponentInParent<Faction>();
        cooldownTimer = Cooldown;
        durationTimer = 0;
        misillePrefab.GetComponent<DamagingProjectile>().damage = Damage;
        boss = transform.parent.Find("Boss").gameObject;
    }

    public  void UseAbility(bool inputReceived){
        if(cooldownTimer >=(Cooldown-0.0001f) && inputReceived){
            StartCoroutine(LastResortCoroutine(misillesPerSecond));
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    
    IEnumerator LastResortCoroutine(float misillesPerSecond){
        durationTimer = 0;
        while(durationTimer < AbilityDuration){
            ShootBullet();
            cooldownTimer = 0;
            durationTimer += 1f/misillesPerSecond;
            yield return new WaitForSeconds(1f/misillesPerSecond);
        }
    }

    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(misillePrefab).GetComponent<Rigidbody2D>();
        bulletRigidBody.transform.parent = this.transform;
        bulletRigidBody.transform.position = new Vector3(faction.player.transform.position.x - 5, boss.transform.position.y);
        bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(misilleVelocityX, 0);
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}

