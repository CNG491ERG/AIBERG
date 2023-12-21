using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BossAgent : Agent{
    private Boss boss;
    private Rigidbody2D bossRb;
    private IBossAbility moveUp;
    private IBossAbility moveDown;
    private IBossAbility basicAttack;
    [SerializeField] private Player player;
    public override void Initialize() {
        bossRb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        player = FindAnyObjectByType<Player>();
        moveUp = GetComponent<MoveUp>();
        moveDown = GetComponent<MoveDown>();
        basicAttack = GetComponent<BasicAttack>();
        bossRb.gravityScale = 0;

        player.OnDamageableHurt += Player_OnDamageableHurt;
        player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableHurt += Boss_OnDamageableHurt;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
    }

    public override void OnEpisodeBegin() {
        transform.localPosition = new Vector3(5.7797966f, 0.806096554f, -2.32154632f);
        player.transform.localPosition = new Vector3(-9.99020386f, 0.806096554f, -2.32154632f);
        boss.Health = 100;
        boss.Defense = 0;
        boss.speed = 10;
        GameManager.Instance.ResetStepCounter();
    }

    private void Boss_OnDamageableDeath(object sender, EventArgs e)
    {
        AddReward(-10f);
        EndEpisode();
    }

    private void Boss_OnDamageableHurt(object sender, EventArgs e)
    {
        AddReward(-0.05f);
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e)
    {
        AddReward(10f);
        EndEpisode();
    }

    private void Player_OnDamageableHurt(object sender, EventArgs e)
    {
        AddReward(0.05f);
    }

    public override void OnActionReceived(ActionBuffers actions){
        int moveAction = actions.DiscreteActions[0];
        int attackAction = actions.DiscreteActions[1];

        moveDown.UseAbility(moveAction == 1);
        moveUp.UseAbility(moveAction == 2);
        basicAttack.UseAbility(attackAction == 1);
    }


    public override void CollectObservations(VectorSensor sensor){
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
        var discreteActionsOut = actionsOut.DiscreteActions;
        
        discreteActionsOut[0] = 0; //No movement
        discreteActionsOut[0] = moveDownInput ? 1 : discreteActionsOut[0];
        discreteActionsOut[0] = moveUpInput ? 2 : discreteActionsOut[0];
        discreteActionsOut[1] = 0; //No attack
        discreteActionsOut[1] = basicAttackInput ? 1 : discreteActionsOut[1];
    }
}
