using System.Linq;
using AIBERG.API;
using AIBERG.BossAbilities;
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
        float timer;
        bool leaderboardVisible = false;
        public override void EnterState(BossModeStateManager stateManager)
        {
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;
            player.inputHandler.enabled = false;
            stateManager.gameEnvironment.StopCountingSteps();
            float groundY = stateManager.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y + 1;
            Sequence playerSequence = DOTween.Sequence();
            playerSequence.Append(player.transform.DOLocalMoveY(groundY, 0.5f).SetEase(Ease.InCubic));
            playerSequence.Append(player.transform.DOLocalMoveX(stateManager.gameEnvironment.bossOffScreenPosition.transform.position.x, 3.0f).SetEase(Ease.InCubic));
            playerSequence.OnComplete(() =>
            {
                playerMovementComplete = true;
                player.gameObject.SetActive(false);
            });

            timer = 0;
            boss.transform.DOLocalMove(stateManager.gameEnvironment.bossOffScreenPosition.position, 3.0f).SetEase(Ease.InBack).OnComplete(() =>
            {
                bossMovementComplete = true;
                AttackDrone[] drones = GameObject.FindObjectsOfType<AttackDrone>();
                foreach (AttackDrone d in drones)
                {
                    d.TakeDamage(1000000f);
                }
                boss.gameObject.SetActive(false);
            });

            UserInformation.Instance.win = true;
            UserInformation.Instance.timetaken = stateManager.gameEnvironment.StepCounter;
            stateManager.gameOverSign.gameObject.SetActive(true);
            stateManager.gameOverSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            stateManager.gameOverSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
            //UserInformation.Instance.score = ...; 
            stateManager.inputRecorder.SendInputData();
        }


        public override void UpdateState(BossModeStateManager stateManager)
        {
            if (playerMovementComplete && bossMovementComplete)
            {
                Debug.Log("Boss mode is complete - player won, do whatever is next!");
            }

            if (timer >= 2.0f)
            {
                stateManager.parallaxController.backgroundSpeed = stateManager.parallaxController.backgroundSpeed > 0 ? stateManager.parallaxController.backgroundSpeed - Time.deltaTime*4.0f : 0;
                if (!leaderboardVisible)
                {
                    leaderboardVisible = true;
                    stateManager.retryButton.SetActive(true);
                    stateManager.gameModesButton.SetActive(true);
                    stateManager.leaderboard.ShowLeaderBoard();
                }

            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
