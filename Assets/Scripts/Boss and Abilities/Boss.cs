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
    Rigidbody2D theBoss;

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
        player = GetComponent<Player>();
        theBoss = this.GetComponent<Rigidbody2D>();
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

    private void UpForce(){
        theBoss.AddForce(new Vector2(0, speed));
    }
    public override void OnActionReceived(ActionBuffers actions){
        //UpForce();
        //ContinuousActions[0] is "Do Nothing"
        int moveDown = actions.DiscreteActions[1];
        int moveUp = actions.DiscreteActions[2];
        int basicAttack = actions.DiscreteActions[3];



        if (moveUp == 1){
            //transform.position = new Vector2(0,moveUp) * Time.deltaTime*2f;
            //up.UseAbility(true);

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
        //Location of the Player and the Boss is Branch 0
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);

        //Health bars of Player and Boss
        sensor.AddObservation(health);
        Debug.Log("PLAYER HEALTH:" + player.Health);
        sensor.AddObservation(player.Health);
        //Raycast Sensors will return positions of attacks, no need to add them to here, neither to components*/
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        Debug.Log("HEURISTIC");
        // Check if the UpArrow key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up Arrow Pressed");
            // Move the object up by moveDistance units
            transform.position += new Vector3(0f, speed, 0f);
        }

        // Check if the DownArrow key is pressed
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow Pressed");
            // Move the object down by moveDistance units
            transform.position -= new Vector3(0f, speed, 0f);
            
        }
        /*ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
            if (Input.GetKey(KeyCode.UpArrow) == true)
                continuousActions[0] = 1;

            var discreteActionsOut = actionsOut.DiscreteActions;
            //discreteActionsOut[1] = Input.GetAxisRaw("Horizontal");
            discreteActionsOut[1] = Input.GetKeyDown(KeyCode.DownArrow).CompareTo(true);
            discreteActionsOut[2] = (Input.GetKeyUp(KeyCode.UpArrow) ? 1 : 0);
            //discreteActionsOut[3] = Input.GetAxis("Vertical");*/
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
