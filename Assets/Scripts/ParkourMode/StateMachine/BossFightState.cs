using AIBERG.Core;
using UnityEngine;

namespace AIBERG.ParkourMode.States{
    public class BossFightState : BaseState
    {
        Player player;
        Boss boss;
        float timed;
        public override void EnterState(GameStateMachineScript stateMachine){
            Debug.Log("BossFight");
            timed = Time.time;
            bossCounter++;                                                                  //num of times boss came across
            Debug.Log(stateMachine.player.Health);
            Player player = stateMachine.player;                 //get player health
            if (player == null)
                Debug.Log("player is null in bosfight");
            Boss boss = stateMachine.boss;                     //get boss health
            boss.gameObject.SetActive(true);
        }
        public override void UpdateState(GameStateMachineScript stateMachine){
            if (player.Health <= 0)                                                          //if player health is 0
                stateMachine.SwitchState(stateMachine.GameOver);                            //go to game over state
            else if (boss.Health <= 0) {                                                //if boss health is 0
                    stateMachine.SwitchState(stateMachine.BossFightToParkour);                  //switch to boss figt to parkour state
            }
            else if(timed - Time.deltaTime >= 5f) {                                 //FOR TESTING   
                stateMachine.SwitchState(stateMachine.BossFightToParkour);
            }
                
        }
     }

}

