using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BossAgent : Agent{
    private Boss boss;
    private Rigidbody2D bossRb;
    private IBossAbility moveUp;
    private IBossAbility moveDown;
    private IBossAbility basicAttack;
    private IBossAbility spawnAttackDrones;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform bossSpawnPosition; 
    [SerializeField] public MLAgentEnvironment env;
    public override void Initialize() {
        bossRb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        moveUp = GetComponent<MoveUp>();
        moveDown = GetComponent<MoveDown>();
        basicAttack = GetComponent<BasicAttack>();
        spawnAttackDrones = GetComponent<SpawnAttackDrones>();
        bossRb.gravityScale = 0;

        player.OnDamageableHurt += Player_OnDamageableHurt;
        player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableHurt += Boss_OnDamageableHurt;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
        player.OnDamageableHurtBasic += Player_OnDamageableHurtBasic;
    }

    public override void OnEpisodeBegin() {
        transform.localPosition = bossSpawnPosition.transform.localPosition;
        player.transform.localPosition = playerSpawnPosition.transform.localPosition;
        player.Health = 100;
        boss.Health = 100;
        boss.Defense = 0;
        boss.speed = 10;
        GameManager.Instance.ResetStepCounter();
        player.ResetAllCooldowns();
        env.RemoveSpawnedObjects();
        //Everything on the screen should be deleted, except for the boss and the player
    }

    private void Player_OnDamageableHurtBasic(object sender, EventArgs e)
    {
        AddReward(-0.15f);
    }
    private void Boss_OnDamageableDeath(object sender, EventArgs e)
    {
        AddReward(-1f);
        EndEpisode();
    }

    private void Boss_OnDamageableHurt(object sender, EventArgs e)
    {
        AddReward(-0.0008f);
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e)
    {
        AddReward(1f);
        EndEpisode();
    }

    private void Player_OnDamageableHurt(object sender, EventArgs e)
    {
        AddReward(0.005f);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        int moveAction = actions.DiscreteActions[0];
        int attackAction = actions.DiscreteActions[1];

        moveDown.UseAbility(moveAction == 1);
        moveUp.UseAbility(moveAction == 2);
        basicAttack.UseAbility(attackAction == 1);
        spawnAttackDrones.UseAbility(attackAction == 2);
    }


    public override void CollectObservations(VectorSensor sensor) {
        //Localposition because everything is under a parent gameobject called "env"
        sensor.AddObservation(bossRb.transform.localPosition.x);
        sensor.AddObservation(bossRb.transform.localPosition.y);
        sensor.AddObservation(boss.Health);

        sensor.AddObservation(player.transform.localPosition.x);
        sensor.AddObservation(player.transform.localPosition.y);
        sensor.AddObservation(player.Health);
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        bool moveDownInput = Input.GetKey(KeyCode.DownArrow);
        bool moveUpInput = Input.GetKey(KeyCode.UpArrow);
        bool basicAttackInput = Input.GetKey(KeyCode.X);
        bool spawnAttackDronesInput = Input.GetKey(KeyCode.C);

        var discreteActionsOut = actionsOut.DiscreteActions;
        
        discreteActionsOut[0] = 0; //No movement
        discreteActionsOut[0] = moveDownInput ? 1 : discreteActionsOut[0];
        discreteActionsOut[0] = moveUpInput ? 2 : discreteActionsOut[0];
        discreteActionsOut[1] = 0; //No attack
        discreteActionsOut[1] = basicAttackInput ? 1 : discreteActionsOut[1];
        discreteActionsOut[1] = spawnAttackDronesInput ? 2 : discreteActionsOut[1];
    }
    
    private void FixedUpdate() {
        if(StepCount%50 == 0){
            AddReward(-0.001f); //To make kill in less time
        }
        Debug.Log(bossRb.velocity.y);
        if(bossRb.velocity.y < 2.5f && bossRb.velocity.y > -2.5f){
            AddReward(-0.08f); //if the boss is too stationary
        }

    }
}
