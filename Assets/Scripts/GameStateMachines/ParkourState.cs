using AIBERG.Core;
using UnityEngine;


namespace AIBERG.GameStateMachines{
public class ParkourState : BaseState
{
    [SerializeField] Player player;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        stateStartTime = Time.deltaTime;
        parkourCounter++;                                                   //number of times parkour happened increased
        stateStartTime = Time.time;                                         //state start time
        player = environment.GetComponent<Player>();                        //gets player obj
    }       
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        if (player.Health <=0)                                              //if player health is <=0
            stateMachine.SwitchState(stateMachine.GameOver);                //go to game over state
        else if ((Time.time - stateStartTime)>=120)                         //after 2 minutes boss arrives
            stateMachine.SwitchState(stateMachine.ParkourToBossFight);      //when player survives 2 minutes go boss fight mode
    }

}

}
