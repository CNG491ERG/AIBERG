using UnityEngine;
using AIBERG.UI;

namespace AIBERG.ParkourMode.States
{
    public class ParkourToBossFightState : BaseState
    {
        TextManager t;

        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateStartTime = Time.time;                                     // time at entering the state
            //t.TextEdit("Boss is Coming!!");                                 //calling fucntion from TextManager to edit the text to say Boss is Coming!!!
            ObstacleHandler(stateMachine, false);
            ObstacleCleaner();
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if ((Time.time - stateStartTime) >= 2.0f) 
            {
                if (stateMachine.BossFight != null) 
                {
                    stateMachine.SwitchState(stateMachine.BossFight);
                }
                else {
                    Debug.LogError("BossFight state is null!");
                }
            }
        }

    }
}

