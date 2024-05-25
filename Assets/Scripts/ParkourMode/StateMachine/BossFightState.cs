using AIBERG.Core;
using AIBERG.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class BossFightState : BaseState
    {
        // Scoring system for the boss fight
        private int bossStateCounter = 0;
        private int bossDefeatBonus = 10000;
        private int timeAliveBonusPerSecond = 50;
        private int damageBonusPerHit = 100;
        // ---------------------------------
        private GameEnvironment environment;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            environment = stateMachine.gameEnvironment;
            bossStateCounter++;
            Debug.Log("BossStateCounter: " + bossStateCounter);
            stateMachine.dangerSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f,1f, 1f, 0f), 0.5f);
            stateMachine.gameEnvironment.Boss.OnDamageableHurt += Boss_OnDamageableHurt;
        }

        private void Boss_OnDamageableHurt(object sender, IDamageable.DamageEventArgs e)
        {
            // Damage Bonus
            environment.scoreCounter?.AddScore(damageBonusPerHit*(long)e.Damage*bossStateCounter);
        }

        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            // Time Alive Bonus
            stateMachine.gameEnvironment.scoreCounter?.AddScore((long)(timeAliveBonusPerSecond * Time.deltaTime * bossStateCounter));
            if (stateMachine.gameEnvironment.Player.Health <= 0){
                stateMachine.SwitchState(stateMachine.GameOver);
            }                                                        
            else if (stateMachine.gameEnvironment.Boss.Health <= 0) {
                // Boss Deafeat Bonus
                stateMachine.gameEnvironment.scoreCounter?.AddScore((long)(bossDefeatBonus * bossStateCounter));
                // Time Efficiency Bonus
                stateMachine.gameEnvironment.scoreCounter?.AddScore((long)(bossDefeatBonus * (stateMachine.gameEnvironment.MaxSteps - stateMachine.gameEnvironment.StepCounter) / stateMachine.gameEnvironment.MaxSteps * bossStateCounter));
                // Player Health Bonus
                stateMachine.gameEnvironment.scoreCounter?.AddScore((long)(stateMachine.gameEnvironment.Player.Health * bossStateCounter));

                stateMachine.SwitchState(stateMachine.BossFightToParkour);
            }

        }
    }

}

