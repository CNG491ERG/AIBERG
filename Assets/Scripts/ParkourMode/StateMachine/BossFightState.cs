using AIBERG.Core;
using AIBERG.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class BossFightState : BaseState
    {
        private int bossStateCounter = 0;
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
            environment.scoreCounter?.AddScore(1000*(long)e.Damage*bossStateCounter*bossStateCounter);
        }

        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            stateMachine.gameEnvironment.scoreCounter?.AddScore((long)(250 * Time.deltaTime * bossStateCounter));
            if (stateMachine.gameEnvironment.Player.Health <= 0){
                stateMachine.SwitchState(stateMachine.GameOver);
            }                                                        
            else if (stateMachine.gameEnvironment.Boss.Health <= 0) {                                                        
                stateMachine.SwitchState(stateMachine.BossFightToParkour);
            }

        }
    }

}

