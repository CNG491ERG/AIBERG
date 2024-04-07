using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(GameStateMachineScript stateMachine);
    public abstract void UpdateState(GameStateMachineScript stateMachine);

}
