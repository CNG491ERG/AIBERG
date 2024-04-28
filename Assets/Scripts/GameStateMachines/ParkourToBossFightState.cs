using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.UI;


public class ParkourToBossFightState: BaseState
{
    TextManager t;

    public override void EnterState(GameStateMachineScript stateMachine)
    {
        stateStartTime = Time.time;                                     // time at entering the state
        t.TextEdit("Boss is Coming!!");                                 //calling fucntion from TextManager to edit the text to say Boss is Coming!!!
        GameObject[] obstacles;

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle1");     //Array of Obstacle1 type objects

        foreach (GameObject obstacle in obstacles)                      //for each Obstacle1 type object
        {
            UnityEngine.Object.Destroy(obstacle);                       //destroy
        }
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if ((Time.time - stateStartTime) >= 2.0f)                       //state should last 2 seconds
        {
            stateMachine.SwitchState(stateMachine.BossFight);           //switch to boss fight
        }
    }

}
