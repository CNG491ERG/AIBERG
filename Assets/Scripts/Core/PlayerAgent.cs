using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace AIBERG.Core{
public class PlayerAgent : Agent{
	[Header("References")]
    [SerializeField] private GameEnvironment environment;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    public override void Initialize(){
        environment = Utility.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        Debug.Log("PlayerAgent.Initialize()");
    }

    public override void CollectObservations(VectorSensor sensor){
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
        Debug.Log("PlayerAgent.CollectObservations()");
    }

    public override void OnEpisodeBegin(){
        boss = environment.Boss;
        player = environment.Player;
        player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
        environment.OnMaxStepsReached += Environment_OnMaxStepsReached;
        environment.ResetEnvironment();
        Debug.Log("PlayerAgent.OnEpisodeBegin()");
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        Debug.Log("PlayerAgent.Heuristic()");
        bool basicAttackInput = player.inputHandler.BasicAbilityInput;
        bool activeAbility1Input = player.inputHandler.ActiveAbility1Input;
        bool activeAbility2Input = player.inputHandler.ActiveAbility2Input;
        bool jumpInput = player.inputHandler.JumpInput;

        var discreteActionsOut = actionsOut.DiscreteActions;
        
        discreteActionsOut[0] = 0; //No movement
        discreteActionsOut[0] = jumpInput ? 1 : discreteActionsOut[0];
        discreteActionsOut[1] = 0; //No attack
        discreteActionsOut[1] = basicAttackInput ? 1 : discreteActionsOut[1];
        discreteActionsOut[1] = activeAbility1Input ? 2 : discreteActionsOut[1];
        discreteActionsOut[1] = activeAbility2Input ? 3 : discreteActionsOut[1];
    }

    private void Boss_OnDamageableDeath(object sender, EventArgs e){
        float reward = (float)(0.5 + (player.Health/player.MaxHealth) * 0.5);
        AddReward(reward);
        player.OnDamageableDeath -= Player_OnDamageableDeath;
        boss.OnDamageableDeath -= Boss_OnDamageableDeath;
        environment.OnMaxStepsReached -= Environment_OnMaxStepsReached;
        EndEpisode();
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e){
        float reward = (float)(-0.5 - (boss.Health/boss.MaxHealth) * 0.5);
        AddReward(reward);
        player.OnDamageableDeath -= Player_OnDamageableDeath;
        boss.OnDamageableDeath -= Boss_OnDamageableDeath;
        environment.OnMaxStepsReached -= Environment_OnMaxStepsReached;
        EndEpisode();
    }

    private void Environment_OnMaxStepsReached(object sender, EventArgs e){
        player.OnDamageableDeath -= Player_OnDamageableDeath;
        boss.OnDamageableDeath -= Boss_OnDamageableDeath;
        environment.OnMaxStepsReached -= Environment_OnMaxStepsReached;
        EndEpisode();    
    }

    public override void OnActionReceived(ActionBuffers actions) {
        Debug.Log("PlayerAgent.OnActionReceived()");
        int moveAction = actions.DiscreteActions[0];
        int attackAction = actions.DiscreteActions[1];

        Debug.Log("Player Move Action: " + moveAction);
        Debug.Log("Player Attack Action: " + attackAction);
        player.jump.UseAbility(moveAction == 1);
        player.basicAbility.UseAbility(attackAction == 1);
        player.activeAbility1.UseAbility(attackAction == 2);
        player.activeAbility2.UseAbility(attackAction == 3);
    }
}


}
