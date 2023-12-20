using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

public class Boss : Agent, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float defense;
    [SerializeField] private int enragement;
    [SerializeField] public float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Rigidbody2D bossRb;

    
    public int maxDistance = 20;
    MoveUp up;
    MoveDown down;
    BasicAttack attack;

    [SerializeField] private Player player;
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

    public override void Initialize() {
        bossRb = this.GetComponent<Rigidbody2D>();
        up = GetComponent<MoveUp>();
        down = GetComponent<MoveDown>();
        attack = GetComponent<BasicAttack>();
    }

    public override void OnEpisodeBegin() {
        transform.position = new Vector2(8,0);
        targetTransform.position = new Vector2(-8.4f,0);
        health = 100;
        defense = 0;
        speed = 10;
        enragement = 1;
    }

    public void TakeDamage(float damageToTake) {
        Debug.Log("I got hit! (boss)");
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        
        if(Health <= 0){
            AddReward(-1f);
            EndEpisode();
        }
        else{
            AddReward(-(0.02f * totalDamage / this.health));
        }
    }

    public override void OnActionReceived(ActionBuffers actions){
        int moveDown = actions.DiscreteActions[2];
        int moveUp = actions.DiscreteActions[1];
        int basicAttack = actions.DiscreteActions[0];

        up.UseAbility(moveUp == 1);
        down.UseAbility(moveDown == 1);
        attack.UseAbility(basicAttack == 1);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        //Location of the Player and the Boss is Branch 0
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);
        sensor.AddObservation(transform.localPosition.z);

        sensor.AddObservation(targetTransform.localPosition.x);
        sensor.AddObservation(targetTransform.localPosition.y);
        sensor.AddObservation(targetTransform.localPosition.z);

        //Health bars of Player and Boss
        sensor.AddObservation(health);
        Debug.Log("PLAYER HEALTH:" + player.Health);
        sensor.AddObservation(player.Health);
        //Raycast Sensors will return positions of attacks, no need to add them to here, neither to components*/
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        bool moveUpInput = Input.GetKey(KeyCode.UpArrow);
        bool moveDownInput = Input.GetKey(KeyCode.DownArrow);
        bool basicAttackInput = Input.GetKey(KeyCode.X);
        var discreteActionsOut = actionsOut.DiscreteActions;
        
        discreteActionsOut[0] = basicAttackInput ? 1 : 0;
        discreteActionsOut[1] = moveUpInput ? 1 : 0;
        discreteActionsOut[2] = moveDownInput ? 1 : 0;
    }

    void FixedUpdate()
    {
        if (StepCount == MaxStep)
        {// MUST DO MORE SCALABLE MAX HEALTHS
            AddReward(-1f + (player.Health / 100));
            EndEpisode();
        }
        else if ((StepCount / 3000f) - (StepCount / 3000) == 0)
        {
            AddReward(-0.05f * (StepCount / MaxStep));
        }
    }

}
    