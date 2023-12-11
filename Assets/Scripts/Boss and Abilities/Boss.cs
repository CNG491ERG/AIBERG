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
    public int maxDistance = 20;
    MoveUp up;
    MoveDown down;
    BasicAttack attack;

    Player player;
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

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector2(8,0);
        targetTransform.position = new Vector2(-8.4f,0);
        health = 0;
        defense = 0;
        speed = 10;
        enragement = 1;
        speed = 10;
    }

    public void TakeDamage(float damageToTake){
        Debug.Log("I got hit! (boss)");
        float totalDamage = damageToTake * (1 - Defense);
        AddReward(-0.02f * (totalDamage / 100f));
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
        AddReward(-(0.02f * totalDamage / this.health));
        if(Health <= 0){
            AddReward(-1f);
            EndEpisode();
        }
    }

    /*public override void OnActionReceived(ActionBuffers actions)//TO BE COMPLETED... TO BE COMPLETED...
    {
        float moveY = actions.ContinuousActions[0];

        transform.position += new Vector3(0,moveY,0)*Time.deltaTime*speed;

        //Add attacks here
    }*/
    public override void OnActionReceived(ActionBuffers actions){
        //ContinuousActions[0] is "Do Nothing"
        float moveDown = actions.ContinuousActions[1];
        float moveUp = actions.ContinuousActions[2];
        float basicAttack = actions.ContinuousActions[3];

        if (moveUp == 1){
            //transform.position = new Vector2(0,moveUp) * Time.deltaTime*2f;
            up.UseAbility(true);
        }
        else if (moveDown == 1) {
            down.UseAbility(true);
        }
        if(basicAttack == 1) {
            attack.UseAbility(true);
        }
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);

        sensor.AddObservation(health);
        sensor.AddObservation(player.Health);

        /*Location of the Player and the Boss is Branch 0
         Health bars of Player and Boss
         Raycast Sensors will return positions of attacks, no need to add them to here, neither to components*/
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.UpArrow) == true)
            continuousActions[0] = 1;*/

        /*var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
        continuousActionsOut[2] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        continuousActionsOut[3] = Input.GetAxis("Vertical");*/
    }


    // Update is called once per frame
    void Update()
    {
        if(StepCount >= MaxStep){// MUST DO MORE SCALABLE MAX HEALTHS
            AddReward(-1f+(player.Health/100));
            EndEpisode();
        }
        else if((StepCount / 3000f) - (StepCount / 3000) == 0){
            AddReward(-0.05f * (StepCount / MaxStep));
        }
    }
}
