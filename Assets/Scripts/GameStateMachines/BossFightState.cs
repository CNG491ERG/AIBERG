using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BossFightState : BaseState
{
    float playerHealth;
    float bossHealth;
    public override void EnterState(GameStateMachineScript stateMachine){
        float playerHealth = GameObject.Find("Player").GetComponent<Player>().Health;
        float bossHealth = GameObject.Find("Boss").GetComponent<Boss>().Health;
    }
    public override void UpdateState(GameStateMachineScript stateMachine){
        if (playerHealth <= 0)
            stateMachine.SwitchState(stateMachine.GameOver);
        else if (bossHealth <= 0)
            stateMachine.SwitchState(stateMachine.BossFightToParkour);
    }
}

