using AIBERG.Core;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class BossFightToParkourState : BaseState
    {
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateStartTime = Time.time;//time entered the state
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if ((Time.time - stateStartTime) >= 2.0f)
            {//state should last 2 seconds
                Debug.Log("BossToParkour");
                Boss boss = stateMachine.boss;                     //get boss health
                boss.gameObject.SetActive(false);
                stateMachine.SwitchState(stateMachine.Parkour);
            }
        }
    }
}

