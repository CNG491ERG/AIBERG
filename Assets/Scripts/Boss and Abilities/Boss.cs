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
    [SerializeField] private float health;
    [SerializeField] private float defense;
    [SerializeField] private int enragement;
    [SerializeField] public float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float goal;


    public override void Initialize()
    {
        //if agent is not in training, no step limit
        if (!Academy.Instance.IsCommunicatorOn){
            this.MaxStep = 0;
        }
        //if agent is in training, 30k step limit
        else
            this.MaxStep = 30000;
        goal = 0;
    }



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
        if (Health == 0){
            AddReward(-1f);
            EndEpisode();
        }
        else
            AddReward(-(0.02f* totalDamage/this.health));

    }

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        defense = 0;
        speed = 10;
        enragement = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(StepCount == MaxStep)
            EpisodeInterrupted();
        if((StepCount/3000f) - (StepCount / 3000) == 0)
            AddReward(-0.05f * (StepCount / MaxStep));

    }
}
