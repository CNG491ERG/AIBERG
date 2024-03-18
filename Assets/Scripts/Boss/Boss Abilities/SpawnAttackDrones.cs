using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnAttackDrones : MonoBehaviour, IAttackAbility{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;

    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 75f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private bool canBeUsed;
    [SerializeField] private List<GameObject> aliveDrones = new();

    [Header("Drone Properties")]
    [SerializeField] private GameObject attackDronePrefab;
    [SerializeField] private List<Transform> attackDroneSpawnPositions;
    [SerializeField] private float attackDroneMaxHealth = 5f;
    [SerializeField] private float attackDroneDefense = 0f;
    [SerializeField] private float attackDroneMaxVelocity = 7.5f;
    [SerializeField] private float attackDroneMovementForce = 10f;
    
    [Header("Drone Projectile Properties")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileDamage = 0.6f;
    [SerializeField] private float projectileSpeed = 8f;
    [SerializeField] private string tagToDamage = "Player";

    #region interface properties
    public float Cooldown => abilityCooldown;
    public float Damage => projectileDamage;
    public float AbilityDuration =>0f;
    public bool CanBeUsed => canBeUsed;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    public GameObject AbilityOwner => boss.gameObject;
    private IAbility abilityLock;
    #endregion

    private void Awake() {
        SetupProjectilePrefab();
        SetupAttackDronePrefab();
    }
    void Start(){
        boss = Utility.ComponentFinder.FindComponentInParents<Boss>(this.transform);
        attackDroneSpawnPositions = boss.DroneTargetPositions;
        ResetCooldown();
        AbilityLock = this;
    }

    private void FixedUpdate() {
        canBeUsed = cooldownTimer >= (Cooldown-0.001f);
        cooldownTimer = canBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public void UseAbility(bool inputReceived){
        if(inputReceived && canBeUsed){
            StartCoroutine(SpawnAttackDronesCoroutine());
        }
    }
    IEnumerator SpawnAttackDronesCoroutine(){
        foreach(Transform spawnPosition in attackDroneSpawnPositions){
            if(aliveDrones.Count < 3){
                AttackDrone attackDrone = Instantiate(attackDronePrefab, boss.Environment.transform).GetComponent<AttackDrone>();
                boss.Environment.AddObjectToEnvironmentList(attackDrone.gameObject);
                aliveDrones.Add(attackDrone.gameObject);
                if(attackDrone != null){
                    attackDrone.transform.position = boss.transform.position;
                    attackDrone.TargetPosition = spawnPosition;
                    cooldownTimer = 0;
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        cooldownTimer = 0;
    }
    
    private void SetupAttackDronePrefab(){
        attackDronePrefab.GetComponent<AttackDrone>().MaxHealth = attackDroneMaxHealth;
        attackDronePrefab.GetComponent<AttackDrone>().Defense = attackDroneDefense;
        attackDronePrefab.GetComponent<AttackDrone>().MaxVelocity = attackDroneMaxVelocity;
        attackDronePrefab.GetComponent<AttackDrone>().MovementForce = attackDroneMovementForce;
        attackDronePrefab.GetComponent<AttackDrone>().ProjectilePrefab = projectilePrefab;
        attackDronePrefab.GetComponent<AttackDrone>().ShootingSpeed = projectileSpeed;
    }

    private void SetupProjectilePrefab(){
        projectilePrefab.GetComponent<DamagingProjectile>().damage = projectileDamage;
        projectilePrefab.GetComponent<DamagingProjectile>().tagToDamage = tagToDamage;
    }

    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }

    public void DestroyAllAliveDrones(){
        foreach(GameObject aliveDrone in aliveDrones){
            Destroy(aliveDrone);
        }
    }
}
