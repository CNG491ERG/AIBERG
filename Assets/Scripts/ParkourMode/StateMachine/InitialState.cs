using AIBERG.API;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class InitialState : BaseState
    {
        private float stateTimer;
        private float stateDuration = 2.0f;
        private Player player;
        private bool startMoving;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateMachine.gameEnvironment.StopCountingSteps();
            stateMachine.gameEnvironment.scoreCounter.canAddScore = true;
            startMoving = true;
            player = stateMachine.gameEnvironment.Player;
            UserInformation.Instance.ResetUserInformation();
            UserInformation.Instance.playMode = false;
            player.inputHandler.enabled = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            stateTimer = 0f;
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if(startMoving){
                player.transform.position = stateMachine.gameEnvironment.playerOffScreenPosition.position;
                player.transform.DOLocalMove(stateMachine.gameEnvironment.PlayerSpawnPosition.localPosition, 1.5f).SetEase(Ease.OutQuad);
                startMoving = false;
            }
            if(stateTimer >= stateDuration){
                stateMachine.SwitchState(stateMachine.Parkour);
            }
            else{
                stateTimer += Time.deltaTime;
            }
            if(player.bodyAnimator != null){
                player.bodyAnimator.SetBool("isFlying", true);
            }
            if(player.thrustersAnimator != null){
                player.thrustersAnimator.SetBool("isFlying", true);
            }
        }
    }
}

