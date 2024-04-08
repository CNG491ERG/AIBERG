using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ParkourState : BaseState
{
    float playerHealth;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        sceneLoader.LoadScene("Parkour");
        stateStartTime = Time.time;
        float playerHealth = GameObject.Find("Player").GetComponent<Player>().Health; 
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if (playerHealth <= 0)
            stateMachine.SwitchState(stateMachine.GameOver);            //when player dies go game over state
        else if ((Time.time - stateStartTime)>120)                      //after 2 minutes boss arrives
            stateMachine.SwitchState(stateMachine.ParkourToBossFight);  //when player survives 2 minutes go boss fight mode
    }
    public  void CollisionState(GameStateMachineScript stateMachine)    //when player collides with something go game over
    {
        stateMachine.SwitchState(stateMachine.GameOver);
    }
}
