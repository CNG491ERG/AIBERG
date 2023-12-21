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
    [SerializeField] private Player player;
    public override void Initialize() {
        bossRb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        player = FindAnyObjectByType<Player>();
        moveUp = GetComponent<MoveUp>();
        moveDown = GetComponent<MoveDown>();
        basicAttack = GetComponent<BasicAttack>();
    }

    public override void OnEpisodeBegin() {
        transform.position = new Vector2(8,0);
        player.transform.position = new Vector2(-8.4f,0);
        boss.Health = 100;
        boss.Defense = 0;
        boss.speed = 10;
        GameManager.Instance.ResetStepCounter();
    }

    public override void OnActionReceived(ActionBuffers actions){
        int moveDownInput = actions.DiscreteActions[2];
        int moveUpInput = actions.DiscreteActions[1];
        int basicAttackInput = actions.DiscreteActions[0];

        moveUp.UseAbility(moveUpInput == 1);
        moveDown.UseAbility(moveDownInput == 1);
        basicAttack.UseAbility(basicAttackInput == 1);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(bossRb.transform.position.x);
        sensor.AddObservation(bossRb.transform.position.y);
        sensor.AddObservation(boss.Health);

        sensor.AddObservation(player.transform.position.x);
        sensor.AddObservation(player.transform.position.y);
        sensor.AddObservation(player.Health);
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

    private void FixedUpdate() {
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
