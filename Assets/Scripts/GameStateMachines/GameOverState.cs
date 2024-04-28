using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using AIBERG.GameStateMachines;

namespace AIBERG.GameStateMachines{
public class GameOverState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        Debug.Log("Entered game over");
        GameObject[] obstacles;

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle1");     //Array of Obstacle1 type objects

        foreach (GameObject obstacle in obstacles)                      //for each Obstacle1 type object
        {
            UnityEngine.Object.Destroy(obstacle);                       //destroy
        }
        /*Should be discussed about how to navigate through after-game*/
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        /*Should be discussed about how to navigate through after-game*/
    }

}

}
