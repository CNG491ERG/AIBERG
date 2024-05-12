using UnityEngine;

namespace AIBERG.ParkourMode.States{
public class InitialState : BaseState
{
   float timeZero;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        
        timeZero = Time.time;
        ObstacleHandler(stateMachine, false);
        GameObject.Find("Player_Game").SetActive(true);
        GameObject.Find("Boss_GameAgent").SetActive(false);
        }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
        Debug.Log("update initial");
        if(Time.time - timeZero > 2.5f)                               //when 2.5 second treshold is passed
            stateMachine.SwitchState(stateMachine.Parkour);         //load parkour
    }
}
}

