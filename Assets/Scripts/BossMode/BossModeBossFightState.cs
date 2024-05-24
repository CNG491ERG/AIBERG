using System;
using AIBERG.Core;
using AIBERG.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeBossFightState : BossModeBaseState
    {   
        private Player player;
        private Boss boss;
        private GameEnvironment environment;
        public override void EnterState(BossModeStateManager stateManager){
            environment = stateManager.gameEnvironment;
            environment.StartCountingSteps();
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;

            player.inputHandler.enabled = true;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            stateManager.dangerSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f,1f, 1f, 0f), 0.5f);
            
            boss.OnDamageableHurt += Boss_OnDamageableHurt;
        }

        private void Boss_OnDamageableHurt(object sender, IDamageable.DamageEventArgs e)
        {
            environment.scoreCounter.AddScore(1000*(long)e.Damage);
        }

        public override void UpdateState(BossModeStateManager stateManager){
            if(Input.GetKeyDown(KeyCode.Escape)){
                (player as IDamageable).TakeDamage(100000f);
            }
            if(Input.GetKeyDown(KeyCode.F1)){
                (boss as IDamageable).TakeDamage(100000f);
            }

            environment.scoreCounter?.AddScore((long)(2500 * Time.deltaTime));

            if(environment.StepCounter == environment.MaxSteps){
                Debug.Log("Max steps reached");
                stateManager.SwitchState(stateManager.gameOverState);
            }
            if(boss.Health <= 0){
                Debug.Log("Boss health < 0");
                stateManager.SwitchState(stateManager.gameOverState); 
            }
            if(player.Health <= 0){
                Debug.Log("Player health < 0");
                stateManager.SwitchState(stateManager.deathState);

            }
        }
    }
}
