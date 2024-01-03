using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IBossAbility, IAttackAbility{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileVelocityX;
    [SerializeField] private Boss boss;
    [SerializeField] private float cooldownTimer;
    public bool CanBeUsed => cooldownTimer >= Cooldown-0.0001f;

    public string AbilityName => "BasicAttack";

    public GameObject AbilityOwner => boss.gameObject;

    public float Cooldown => 0.5f;

    public float Damage => 5f;

    public float AbilityDuration => 0;
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
        boss = GetComponent<Boss>();
        this.projectilePrefab.GetComponent<DamagingProjectile>().damage = Damage;
    }

    public void UseAbility(bool inputReceived)
    {
        if(cooldownTimer >= Cooldown-0.0001f && inputReceived){
            Rigidbody2D projectileRb = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
            projectileRb.transform.parent = this.transform;
            projectileRb.transform.localPosition = Vector3.zero;
            projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(-projectileVelocityX, 0);
            cooldownTimer = 0;
            Debug.Log("Projectile velocity: " + projectileRb.velocity);
        }
        cooldownTimer = cooldownTimer >= (Cooldown-0.0001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    public void ResetCooldown(){
        cooldownTimer = 0;
    }

}
