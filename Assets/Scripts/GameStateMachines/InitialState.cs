using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : BaseState
{
    float timeZero = Time.time;
    public override void EnterState(GameStateMachineScript stateMachine)
    {
        GameObject.Find("Boss").SetActive(false);                   //sets boss diasbled
        //GameObject.Find("Player").SetActive(false);                 //sets player inactive
        //Score counter should start counting
        //time counter should start counting
    }
    public override void UpdateState(GameStateMachineScript stateMachine)
    {
      if(Time.time - timeZero > 2.5f)                               //when 2.5 second treshold is passed
            stateMachine.SwitchState(stateMachine.Parkour);         //load parkour
    }
}
