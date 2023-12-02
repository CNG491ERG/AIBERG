using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : BaseAbility{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileVelocityX;
    [SerializeField] private Boss boss;
    [SerializeField] private float cooldownTimer;
    public bool CanBeUsed{
        get{
            return cooldownTimer >= cooldown-0.0001f;
        }
    }
    private void Start() {
        boss = GetComponent<Boss>();
        this.abilityName = "BasicAttack";
        this.abilityOwner = boss.gameObject;
        this.cooldown = 0.5f;
        this.damage = 5f;
        this.projectilePrefab.GetComponent<DamagingProjectile>().damage = this.damage;
    }

    public override void UseAbility(bool inputReceived)
    {
        if(cooldownTimer >= cooldown-0.0001f && inputReceived){
            Rigidbody2D projectileRb = Instantiate(projectilePrefab).GetComponent<Rigidbody2D>();
            projectileRb.transform.parent = this.transform;
            projectileRb.transform.localPosition = Vector3.zero;
            projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(-projectileVelocityX, 0);
            cooldownTimer = 0;
            Debug.Log("Projectile velocity: " + projectileRb.velocity);
        }
        cooldownTimer = cooldownTimer >= (cooldown-0.0001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

}
