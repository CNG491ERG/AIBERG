using System;
using AIBERG.Interfaces;
using AIBERG.Core;
using UnityEngine;

namespace AIBERG.BossAbilities{
    public class AttackDrone : MonoBehaviour, IDamageable{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;

    [Header("Player Reference")]
    [SerializeField] private Player player;

    [Header("Attack Drone Rigidbody Reference")]
    [SerializeField] private Rigidbody2D attackDroneRb;

    [Header("Attack Drone Attributes")]
    [SerializeField] private float maxVelocity;
    [SerializeField] private float movementForce;
    [SerializeField] private float health;
    [SerializeField] private float defense;
    [SerializeField] private float maxHealth;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float damage;
    [SerializeField] private float shootingSpeed;

    [Header("Projectile Prefab")]
    [SerializeField] private GameObject projectilePrefab;

    #region Attack Drone Properties
    public float MaxVelocity{get => maxVelocity; set => maxVelocity = value;}
    public float MovementForce{get => movementForce; set => movementForce = value;}
    public float Health {get => health; set => health = value;}
    public float Defense {get => defense; set => defense = value;}
    public float MaxHealth {get => maxHealth; set => maxHealth = value;}
    public Transform TargetPosition{get => targetPosition; set => targetPosition = value;}
    public float Damage{get => damage; set => damage = value;}
    public GameObject ProjectilePrefab{get => projectilePrefab; set => projectilePrefab = value;}
    public float ShootingSpeed{get => shootingSpeed; set => shootingSpeed = value;}
    #endregion

    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;

    void Start(){
        Health = MaxHealth;
        GameEnvironment e = Utilities.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        boss = e.Boss;
        attackDroneRb = GetComponent<Rigidbody2D>();
    }

    private float shootTimer = 0f;
    void FixedUpdate(){
        if(shootTimer >= 1.5f){
            ShootBullet();
            shootTimer = 0f;
        }
        shootTimer += Time.fixedDeltaTime;
        Vector2 direction = (TargetPosition.position - transform.position).normalized;
        attackDroneRb.AddForce(direction * MovementForce);
        attackDroneRb.velocity = Vector2.ClampMagnitude(attackDroneRb.velocity, MaxVelocity);

    }

    private void ShootBullet(){
        Rigidbody2D projectileRb = Instantiate(projectilePrefab, boss.Environment.transform).GetComponent<Rigidbody2D>();
        boss.Environment.AddObjectToEnvironmentList(projectileRb.gameObject);
        if(projectileRb != null){
            projectileRb.transform.position = transform.position;
            Vector3 projectileShootingDir = (boss.Environment.Player.transform.position - transform.position).normalized;
            projectileRb.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = projectileShootingDir * ShootingSpeed;
            float angle = (Mathf.Atan2(projectileShootingDir.y, projectileShootingDir.x) * Mathf.Rad2Deg)+180f;
            projectileRb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
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

}
