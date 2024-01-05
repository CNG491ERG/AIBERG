using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class SpawnAttackDrones : MonoBehaviour, IBossAbility, IAttackAbility
{
    [SerializeField] private GameObject attackDronePrefab;
    [SerializeField] private Transform[] droneSpawnPositions;
    [SerializeField] private Boss boss;
    [SerializeField] private float cooldownTimer;
    public string AbilityName => "SpawnAttackDrones";

    public GameObject AbilityOwner => boss.gameObject;

    public float Cooldown => 25f;

    public bool CanBeUsed => cooldownTimer >= Cooldown - 0.0001f;

    public float AbilityDuration => 0;

    public IAbility AbilityLock
    {
        get => abilityLock;
        set
        {
            if ((Object)value == (Object)this || value == null)
            {
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;

    //Damage in this case is how much damage a bullet of a drone causes
    public float Damage => 1f;

    void Start()
    {
        boss = GetComponent<Boss>();
        cooldownTimer = Cooldown;
        AbilityLock = this;
    }

    public void ResetCooldown()
    {
        cooldownTimer = 0;
    }

    public void UseAbility(bool inputReceived)
    {
        if(inputReceived && CanBeUsed){
            StartCoroutine(SpawnAttackDronesCoroutine());
        }
        cooldownTimer = CanBeUsed ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
    IEnumerator SpawnAttackDronesCoroutine(){
        //spawn drones (3)
        foreach(Transform droneSpawnPosition in droneSpawnPositions){
            AttackDrone drone = Instantiate(attackDronePrefab).GetComponent<AttackDrone>();
            boss.GetComponent<BossAgent>().env.AddObject(drone.gameObject);
            if(drone != null){
                drone.transform.position = boss.transform.position;
                drone.targetPosition = droneSpawnPosition;
                drone.boss = boss;
                drone.damage = Damage;
                cooldownTimer = 0;
                yield return new WaitForSeconds(0.2f);
            }
        }
        cooldownTimer = 0;
    }
}
