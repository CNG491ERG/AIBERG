using System.Collections;
using UnityEngine;


public class SpawnAttackDrones : MonoBehaviour, IAttackAbility{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;

    [Header("Ability Properties")]
    [SerializeField] private float abilityCooldown = 75f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private float abilityDuration = 4.0f;
    [SerializeField] private float durationTimer = 0f;
    [SerializeField] private bool canBeUsed;

    [Header("Drone Properties")]
    [SerializeField] private GameObject attackDronePrefab;
    [SerializeField] private Transform[] droneSpawnPositions;
    [SerializeField] private float attackDroneDamage = 0.6f;

    #region interface properties
    public float Cooldown => abilityCooldown;
    public float Damage => attackDroneDamage;
    public float AbilityDuration => abilityDuration;
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


    void Start(){
        boss = Utility.ComponentFinder.FindComponentInParents<Boss>(this.transform);
        ResetCooldown();
        AbilityLock = this;
    }

    public void UseAbility(bool inputReceived){
        if(inputReceived && CanBeUsed){
            StartCoroutine(SpawnAttackDronesCoroutine());
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    IEnumerator SpawnAttackDronesCoroutine(){
        foreach(Transform droneSpawnPosition in droneSpawnPositions){
            AttackDrone attackDrone = Instantiate(attackDronePrefab).GetComponent<AttackDrone>();
            if(attackDrone != null){
                attackDrone.transform.position = boss.transform.position;
                attackDrone.targetPosition = droneSpawnPosition;
                attackDrone.boss = boss;
                attackDrone.damage = Damage;
                cooldownTimer = 0;
                yield return new WaitForSeconds(0.2f);
            }
        }
        cooldownTimer = 0;
    }
    
    public void ResetCooldown(){
        cooldownTimer = Cooldown;
    }
}
