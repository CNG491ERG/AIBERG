using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class Boss : Agent, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float defense;
    [SerializeField] private int enragement;
    [SerializeField] public float speed;
    [SerializeField] private float cooldownMultiplier;
    [SerializeField] private float damageMultiplier;
    [SerializeField] private Transform targetTransform;
    Player player;
    DamagingProjectile projectile;
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
        transform.position = new Vector2(10,0);
        targetTransform.position = new Vector2(-10,0);
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
        if(Health < 0){
            AddReward(-1f);
            EndEpisode();
        }
    }

    public override void OnActionReceived(ActionBuffers actions)//TO BE COMPLETED... TO BE COMPLETED...
    {
        float moveY = actions.ContinuousActions[0];

        transform.position += new Vector3(0,moveY,0)*Time.deltaTime*speed;

        //Add attacks here
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
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Vertical");
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
