using AIBERG.Core;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class BossFightToParkourState : BaseState
    {
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateStartTime = Time.time;//time entered the state
            Debug.Log("entered bosstoparkour");
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if ((Time.time - stateStartTime) >= 2.0f)
            {
                Debug.Log("BossToParkour");
                Boss boss = stateMachine.GetBoss();                     
                boss.gameObject.SetActive(false);
                stateMachine.SwitchState(stateMachine.Parkour);
            }
        }
    }
}

