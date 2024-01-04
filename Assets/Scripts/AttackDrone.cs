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
    private float bulletSpeed = 10f;
    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;
    private float maxHealth = 7.5f;

    private float health;
    private float defense = 0f;
    public float Health { get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public float forceStrength = 1f; // Strength of the force
    public float maxDistance = 5f;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start(){
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private float shootTimer = 0f;
    void FixedUpdate(){
        if(shootTimer >= 0.2f){
            ShootBullet();
            shootTimer = 0f;
        }
        shootTimer += Time.fixedDeltaTime;

        Vector2 direction = (targetPosition.position - transform.position).normalized;

         // Limit distance from the target
        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        // Apply force towards the limited target position
        rb.AddForce(direction.normalized * forceStrength);

    }

    private void ShootBullet(){
        Rigidbody2D bulletRigidBody = Instantiate(attackDroneBulletPrefab).GetComponent<Rigidbody2D>();
        bulletRigidBody.transform.position = transform.position;
        Vector3 bulletShootDirection = (boss.player.transform.position - transform.position).normalized;
        bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = bulletShootDirection * bulletSpeed;
        bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().damage = damage;
    }

    public void TakeDamage(float damageToTake){
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);
        if(Health == 0){
            OnDamageableDeath?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }
    }

}
