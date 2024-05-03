using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AIBERG.ParkourMode.States{
public class GameOverState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        ObstacleHandler(stateMachine, false);
        Debug.Log("Entered game over");
        ObstacleCleaner();
        /*Should be discussed about how to navigate through after-game*/
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        /*Should be discussed about how to navigate through after-game*/
    }

}

}
