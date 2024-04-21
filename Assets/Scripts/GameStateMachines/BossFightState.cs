using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossFightState : BaseState
{
    float playerHealth;
    float bossHealth;
    public override void EnterState(GameStateMachineScript stateMachine){
        bossCounter++;                                                                  //num of times boss came across
        float playerHealth = GameObject.Find("Player").GetComponent<Player>().Health;   //get player health
        float bossHealth = GameObject.Find("Boss").GetComponent<Boss>().Health;         //get boss health
    }
    public override void UpdateState(GameStateMachineScript stateMachine){
        if (playerHealth <= 0)                                                          //if player health is 0
            stateMachine.SwitchState(stateMachine.GameOver);                            //go to game over state
        else if (bossHealth <= 0)                                                       //if boss health is 0
            stateMachine.SwitchState(stateMachine.BossFightToParkour);                  //switch to boss figt to parkour state
    }
}

