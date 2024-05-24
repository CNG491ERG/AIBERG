using AIBERG.Core;
using UnityEngine;


namespace AIBERG.ParkourMode.States{
public class ParkourState : BaseState
{
    [SerializeField] Player player;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        ObstacleHandler(stateMachine, true);
        stateStartTime = Time.deltaTime;
        parkourCounter++;                                                   
        stateStartTime = Time.time;                                         
        player = stateMachine.GetPlayer();                        
    }       
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if (player.Health <=0)                                              //if player health is <=0
            stateMachine.SwitchState(stateMachine.GameOver);                //go to game over state
        else if ((Time.time - stateStartTime)>=30)                         //after 30 seconds boss arrives
            stateMachine.SwitchState(stateMachine.ParkourToBossFight);      //when player survives 2 minutes go boss fight mode
    }

}

}
