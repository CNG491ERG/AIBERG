using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeBossFightState : BossModeBaseState
    {   
        private Player player;
        private Boss boss;

        public override void EnterState(BossModeStateManager stateManager){
            stateManager.gameEnvironment.StartCountingSteps();
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;

            player.inputHandler.enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        public override void UpdateState(BossModeStateManager stateManager){
            if(Input.GetKeyDown(KeyCode.Escape)){
                (player as IDamageable).TakeDamage(100000f);
            }
            if(stateManager.gameEnvironment.StepCounter == stateManager.gameEnvironment.MaxSteps){
                stateManager.SwitchState(stateManager.gameOverState);
            }
            if(boss.Health <= 0){
                stateManager.SwitchState(stateManager.gameOverState); 
            }
            if(player.Health <= 0){
                stateManager.SwitchState(stateManager.deathState);

            }
        }
    }
}
