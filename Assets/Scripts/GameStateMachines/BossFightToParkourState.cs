using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossFightToParkourState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        stateStartTime = Time.time;//time entered the state
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if((Time.time - stateStartTime)>=2.0f) {//state should last 2 seconds
            stateMachine.SwitchState(stateMachine.Parkour);
        }
    }

}
