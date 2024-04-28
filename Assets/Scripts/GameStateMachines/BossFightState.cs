using AIBERG.Core;

namespace AIBERG.GameStateMachines{
public class BossFightState : BaseState
{
    Player player;
    Boss boss;
    public override void EnterState(GameStateMachineScript stateMachine){
        bossCounter++;                                                                  //num of times boss came across
        Player player = environment.GetComponent<Player>();                 //get player health
        Boss boss = environment.GetComponent<Boss>();                     //get boss health
    }
    public override void UpdateState(GameStateMachineScript stateMachine){
        if (player.Health <= 0)                                                          //if player health is 0
            stateMachine.SwitchState(stateMachine.GameOver);                            //go to game over state
        else if (boss.Health <= 0)                                                       //if boss health is 0
            stateMachine.SwitchState(stateMachine.BossFightToParkour);                  //switch to boss figt to parkour state
    }
}

}

