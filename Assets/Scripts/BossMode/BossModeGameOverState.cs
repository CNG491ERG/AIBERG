using System.Linq;
using AIBERG.API;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeGameOverState : BossModeBaseState
    {
        private Player player;
        private Boss boss;
        private bool playerMovementComplete = false;
        private bool bossMovementComplete = false;

        public override void EnterState(BossModeStateManager stateManager)
        {
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;  
            player.inputHandler.enabled = false;
            stateManager.gameEnvironment.StopCountingSteps();
            float groundY = stateManager.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y+1;
            Sequence playerSequence = DOTween.Sequence();
            playerSequence.Append(player.transform.DOLocalMoveY(groundY, 0.5f).SetEase(Ease.InCubic));
            playerSequence.Append(player.transform.DOLocalMoveX(stateManager.gameEnvironment.bossOffScreenPosition.transform.position.x, 3.0f).SetEase(Ease.InCubic));
            playerSequence.OnComplete(() => {
                playerMovementComplete = true;
            });

            boss.transform.DOLocalMove(stateManager.gameEnvironment.bossOffScreenPosition.position, 3.0f).SetEase(Ease.InBack).OnComplete(() => {
                bossMovementComplete = true;
            });

            UserInformation.Instance.win = true;
            UserInformation.Instance.timetaken = stateManager.gameEnvironment.StepCounter;
            //UserInformation.Instance.score = ...; 
            stateManager.inputRecorder.SendInputData();
        }

        public override void UpdateState(BossModeStateManager stateManager){
            if(playerMovementComplete && bossMovementComplete){
                Debug.Log("Boss mode is complete - player won, do whatever is next!");
            }
        }
    }
}
