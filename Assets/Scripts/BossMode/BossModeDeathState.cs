using System.Linq;
using AIBERG.API;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeDeathState : BossModeBaseState
    {
        private Player player;
        private Boss boss;
        private bool allMovementComplete = false;
        public override void EnterState(BossModeStateManager stateManager)
        {
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;
            player.inputHandler.enabled = false;
            stateManager.gameEnvironment.StopCountingSteps();
            float endPoint = stateManager.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y + 1;
            player.transform.DOLocalMoveY(endPoint, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                boss.transform.DOLocalMove(stateManager.gameEnvironment.playerOffScreenPosition.position, 1.0f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    allMovementComplete = true;
                });
            });

            UserInformation.Instance.win = false;
            UserInformation.Instance.timetaken = stateManager.gameEnvironment.StepCounter;
            //UserInformation.Instance.score = ...; 
            stateManager.gameOverSign.gameObject.SetActive(true);
            stateManager.gameOverSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            stateManager.gameOverSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f).OnComplete(()=>{
                stateManager.leaderboard.ShowLeaderBoard();
            });
            stateManager.inputRecorder.SendInputData();
        }

        public override void UpdateState(BossModeStateManager stateManager)
        {
            if (allMovementComplete)
            {
                Debug.Log("Boss mode is complete - player died, do whatever is next!");
            }
        }
    }
}
