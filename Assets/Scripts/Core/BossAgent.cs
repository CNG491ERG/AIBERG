using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using AIBERG.Utilities;

namespace AIBERG.Core{
    public class BossAgent : Agent{
    [Header("References")]
    [SerializeField] private GameEnvironment environment;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;

    public override void Initialize() {
        environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        boss = environment.Boss;
        player = environment.Player;
        environment.Player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
        environment.OnMaxStepsReached += Environment_OnMaxStepsReached;
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(boss.transform.localPosition);
        sensor.AddObservation(boss.Health);
        sensor.AddObservation(boss.basicAttackAbility.CanBeUsed);
        sensor.AddObservation(boss.spawnAttackDroneAbility.CanBeUsed);

        sensor.AddObservation(player.transform.localPosition);
        sensor.AddObservation(player.Health);
        sensor.AddObservation(player.activeAbility1.CanBeUsed);
        sensor.AddObservation(player.activeAbility2.CanBeUsed);
        sensor.AddObservation(player.basicAbility.CanBeUsed);

        sensor.AddObservation(environment.StepCounter);
    }

    public override void OnEpisodeBegin(){
        environment.ResetEnvironment();
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        Debug.Log("Inside Boss heuristic");
        bool moveDownInput = Input.GetKey(KeyCode.DownArrow);
        bool moveUpInput = Input.GetKey(KeyCode.UpArrow);
        bool basicAttackInput = Input.GetKey(KeyCode.J);
        bool spawnAttackDronesInput = Input.GetKey(KeyCode.K);

        var discreteActionsOut = actionsOut.DiscreteActions;
        
        discreteActionsOut[0] = 0; //No movement
        discreteActionsOut[0] = moveDownInput ? 1 : discreteActionsOut[0];
        discreteActionsOut[0] = moveUpInput ? 2 : discreteActionsOut[0];
        discreteActionsOut[1] = 0; //No attack
        discreteActionsOut[1] = basicAttackInput ? 1 : discreteActionsOut[1];
        discreteActionsOut[1] = spawnAttackDronesInput ? 2 : discreteActionsOut[1];
    }


    private void Boss_OnDamageableDeath(object sender, EventArgs e){
        float reward = (float)(-0.5 - (player.Health/player.MaxHealth) * 0.5);
        AddReward(reward);
        EndEpisode();
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e){
        float reward = (float)(0.5 + (boss.Health/boss.MaxHealth) * 0.5);
        AddReward(reward);
        EndEpisode();
    }
    private void Environment_OnMaxStepsReached(object sender, EventArgs e){
        EndEpisode();    
    }


    public override void OnActionReceived(ActionBuffers actions) {
        int moveAction = actions.DiscreteActions[0];
        int attackAction = actions.DiscreteActions[1];

        Debug.Log("Boss Move Action: " + moveAction);
        Debug.Log("Boss Attack Action: " + attackAction);
        boss.moveDownAbility.UseAbility(moveAction == 1);
        boss.moveUpAbility.UseAbility(moveAction == 2);
        boss.basicAttackAbility.UseAbility(attackAction == 1);
        boss.spawnAttackDroneAbility.UseAbility(attackAction == 2);
    }
}

}
