using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        sceneLoader.LoadScene("GameOver");
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        /*Should be discussed with Eren about how to navigate through after-game*/
    }

}
