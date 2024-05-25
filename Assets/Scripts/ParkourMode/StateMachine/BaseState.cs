using UnityEngine;
using AIBERG.Core;
using AIBERG.ParkourMode;

namespace AIBERG.ParkourMode.States
{
    public abstract class BaseState
    {
        public abstract void EnterState(GameStateMachineScript stateMachine);
        public abstract void UpdateState(GameStateMachineScript stateMachine);
    }
}
