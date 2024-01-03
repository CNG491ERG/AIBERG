using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float defense;
    [SerializeField] private int enragement;
    [SerializeField] public float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody2D bossRb;
    private IBossAbility moveUp;
    private IBossAbility moveDown;
    private IBossAbility basicAttack;
    [SerializeField] private Player player;

    public event EventHandler OnDamageableDeath;
    public event EventHandler OnDamageableHurt;

    public float Health { 
        get{
            return health;
        }
        set{
            health = value;
        }
    }
    public float Defense { 
        get{
            return defense;
        }
        set{
            defense = value;
        }
    }

    private void Start() {
        moveUp = GetComponent<MoveUp>();
        moveDown = GetComponent<MoveDown>();
        basicAttack = GetComponent<BasicAttack>();
    }
    
    public void TakeDamage(float damageToTake) {
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        OnDamageableHurt?.Invoke(this, EventArgs.Empty);
        if(Health == 0){
            OnDamageableDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
    