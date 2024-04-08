using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected SceneLoader sceneLoader;
    protected float stateStartTime;
    public abstract void EnterState(GameStateMachineScript stateMachine);
    public abstract void UpdateState(GameStateMachineScript stateMachine);

}
