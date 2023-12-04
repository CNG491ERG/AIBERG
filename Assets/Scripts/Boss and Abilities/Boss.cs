using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class Boss : Agent, IDamageable
{
    [SerializeField] private float health = 100;
    [SerializeField] private float defense = 0;
    [SerializeField] private int enragement = 1;
    [SerializeField] public float speed = 10;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;

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

    public void TakeDamage(float damageToTake){
        Debug.Log("I got hit! (boss)");
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
