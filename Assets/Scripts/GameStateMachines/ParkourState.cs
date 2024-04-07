using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {

    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if (player.health <= 0)//FİX
            stateMachine.SwitchState(stateMachine.GameOver);
        else if (playTime >= reference)//FİX
            stateMachine.SwitchState(stateMachine.ParkourToBossFight);
    }
    public  void CollisionState(GameStateMachineScript stateMachine)
    {

    }
}
