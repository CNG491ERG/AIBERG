using UnityEngine;

namespace AIBERG.ParkourMode.States{
public class InitialState : BaseState
{
    float timeZero = Time.time;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        GameObject.Find("SpawnPoint").SetActive(false);
        GameObject.Find("Player_Agent").SetActive(false);
        GameObject.Find("Boss_Agent").SetActive(false);                      //sets boss diasbled
        //Score counter should start counting
        //time counter should start counting
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
      if(Time.time - timeZero > 2.5f)                               //when 2.5 second treshold is passed
            stateMachine.SwitchState(stateMachine.Parkour);         //load parkour
    }
}
}

