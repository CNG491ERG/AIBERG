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

    private float health = 5f;
    private float defense = 0f;
    public float Health { get => health; set => health = value;}
    public float Defense { get => defense; set => defense = value;}

    void Start()
    {
        MoveToTarget();
    }

    private float shootTimer = 0f;
    void FixedUpdate(){
        if(shootTimer >= 0.2f){
            ShootBullet();
            shootTimer = 0f;
        }
        shootTimer += Time.fixedDeltaTime;
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
        }
    }
    void MoveToTarget(){
         float distanceToTarget = Vector3.Distance(transform.position, targetPosition.position);

        // Recalculate the duration based on the distance
        float adjustedDuration = distanceToTarget / 3.5f; 

        this.transform.DOMove(targetPosition.position, adjustedDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => MoveToTarget());
    }
}
