using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackDrone : MonoBehaviour, IDamageable{
    public Transform targetPosition;
    public float damage;
    public Boss boss;
    public GameObject attackDroneBulletPrefab;
    private float bulletSpeed = 8f;
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;
    private float maxHealth = 5f;

    private float health;
    private float defense = 0f;
    public float Health { get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public float forceStrength; // Strength of the force
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public float maxVelocity = 7.5f; // Maximum velocity limit
    void Start(){
        health = maxHealth;
        forceStrength = 10f;
        rb = GetComponent<Rigidbody2D>();
    }

    private float shootTimer = 0f;
    void FixedUpdate(){
        if(shootTimer >= 1.5f){
            ShootBullet();
            shootTimer = 0f;
        }
        shootTimer += Time.fixedDeltaTime;

        Vector2 direction = (targetPosition.position - transform.position).normalized;
        rb.AddForce(direction * forceStrength);
        // Limit the velocity magnitude
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

    }

    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(attackDroneBulletPrefab).GetComponent<Rigidbody2D>();
        boss.GetComponent<BossAgent>().env.AddObject(bulletRigidBody.gameObject);
        if(bulletRigidBody != null){
            bulletRigidBody.transform.position = transform.position;
            Vector3 bulletShootDirection = (boss.player.transform.position - transform.position).normalized;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = bulletShootDirection * bulletSpeed;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().damage = damage;
        }
    }

    public void TakeDamage(float damageToTake){
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);
        if(Health == 0){
            Destroy(this.gameObject);
        }
    }

}
