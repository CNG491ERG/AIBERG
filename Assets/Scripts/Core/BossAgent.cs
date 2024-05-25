using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using AIBERG.Utilities;

namespace AIBERG.Core
{
    public class BossAgent : Agent
    {
        [Header("References")]
        [SerializeField] private GameEnvironment environment;
        [SerializeField] public Player player;
        [SerializeField] public Boss boss;
        [SerializeField] public bool basicAttackInput;
        [SerializeField] public bool attackDroneInput;
        [SerializeField] public bool moveUpInput;
        [SerializeField] public bool moveDownInput;

        public override void Initialize()
        {
            environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
            if(environment.Boss != null){
                boss = environment.Boss;
                boss.OnDamageableDeath += Boss_OnDamageableDeath;
            }
            if(environment.Player != null){
                player = environment.Player;
                player.OnDamageableDeath += Player_OnDamageableDeath;
            }
            environment.OnMaxStepsReached += Environment_OnMaxStepsReached;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(boss.transform.localPosition);
            sensor.AddObservation(boss.Health);
            sensor.AddObservation(boss.basicAttackAbility.CanBeUsed);
            sensor.AddObservation(boss.spawnAttackDroneAbility.CanBeUsed);

            sensor.AddObservation(player.transform.localPosition);
            sensor.AddObservation(player.Health);
            sensor.AddObservation(player.activeAbility1.CanBeUsed);
            sensor.AddObservation(player.activeAbility2.CanBeUsed);
            sensor.AddObservation(player.basicAbility.CanBeUsed);

            //sensor.AddObservation(environment.StepCounter);
        }

        public override void OnEpisodeBegin()
        {
            if (environment.IsTrainingEnvironment)
            {
                environment.ResetEnvironment();
            }
        }

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            Debug.Log("Inside Boss heuristic");
            if (environment.Player.inputHandler != null)
            {
                moveDownInput = Input.GetKey(KeyCode.DownArrow);
                moveUpInput = Input.GetKey(KeyCode.UpArrow);
                basicAttackInput = Input.GetKey(KeyCode.J);
                attackDroneInput = Input.GetKey(KeyCode.K);
            }


            var discreteActionsOut = actionsOut.DiscreteActions;

            discreteActionsOut[0] = 0; //No movement
            discreteActionsOut[0] = moveDownInput ? 1 : discreteActionsOut[0];
            discreteActionsOut[0] = moveUpInput ? 2 : discreteActionsOut[0];
            discreteActionsOut[1] = 0; //No attack
            discreteActionsOut[1] = basicAttackInput ? 1 : discreteActionsOut[1];
            discreteActionsOut[1] = attackDroneInput ? 2 : discreteActionsOut[1];
        }


        private void Boss_OnDamageableDeath(object sender, EventArgs e)
        {
            float reward = (float)(-0.5 - (player.Health / player.MaxHealth) * 0.5);
            AddReward(reward);
            if (environment.IsTrainingEnvironment)
            {
                EndEpisode();
            }
        }

        private void Player_OnDamageableDeath(object sender, EventArgs e)
        {
            float reward = (float)(0.5 + (boss.Health / boss.MaxHealth) * 0.5);
            AddReward(reward);
            if (environment.IsTrainingEnvironment)
            {
                EndEpisode();
            }
        }
        private void Environment_OnMaxStepsReached(object sender, EventArgs e)
        {
            if (environment.IsTrainingEnvironment)
            {
                EndEpisode();
            }
        }


        public override void OnActionReceived(ActionBuffers actions)
        {
            int moveAction = actions.DiscreteActions[0];
            int attackAction = actions.DiscreteActions[1];
            basicAttackInput = attackAction == 1;
            attackDroneInput = attackAction == 2;
            moveDownInput = moveAction == 1;
            moveUpInput = moveAction == 2;

            boss.moveDownAbility.UseAbility(moveDownInput);
            boss.moveUpAbility.UseAbility(moveUpInput);
            boss.basicAttackAbility.UseAbility(basicAttackInput);
            boss.spawnAttackDroneAbility.UseAbility(attackDroneInput);
        }
    }

}
