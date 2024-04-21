using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverState : BaseState
{
    public override void EnterState(GameStateMachineScript stateMachine)
    {
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
