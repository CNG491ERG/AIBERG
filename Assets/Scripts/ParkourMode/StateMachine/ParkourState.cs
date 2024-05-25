using AIBERG.Core;
using UnityEngine;


namespace AIBERG.ParkourMode.States
{
    public class ParkourState : BaseState
    {
        private Player player;
        private GameEnvironment environment;
        private float stateDuration = 30f;
        private float stateDurationTimer;
        private int parkourStateCounter = 0;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateDurationTimer = 0f;
            parkourStateCounter++;
            Debug.Log("ParkourStateCounter: " + parkourStateCounter);
            environment = stateMachine.gameEnvironment;
            player = environment.Player;
            environment.StartCountingSteps();
            stateMachine.obstacleSpawner.StartSpawningObstacles();
            player.inputHandler.enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            environment.scoreCounter?.AddScore((long)(250 * Time.deltaTime * parkourStateCounter));
            if (player.Health <= 0)
            {
                stateMachine.SwitchState(stateMachine.GameOver);
            }
            if (stateDurationTimer >= stateDuration)
            {
                stateMachine.SwitchState(stateMachine.ParkourToBossFight);
            }
            else
            {
                stateDurationTimer += Time.deltaTime;
            }
        }

    }

}
