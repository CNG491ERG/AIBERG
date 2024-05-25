using System.Linq;
using AIBERG.API;
using AIBERG.BossAbilities;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class GameOverState : BaseState
    {
        private Player player;
        private Boss boss;
        float timer;
        bool showLeaderboard = false;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            player = stateMachine.gameEnvironment.Player;
            boss = stateMachine.gameEnvironment.Boss;
            stateMachine.obstacleSpawner.StopSpawningObstacles();
            player.inputHandler.enabled = false;
            stateMachine.gameEnvironment.StopCountingSteps();
            float endPoint = stateMachine.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y + 1;
            player.transform.DOLocalMoveY(endPoint, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                if (boss != null)
                {
                    boss.transform.DOLocalMove(stateMachine.gameEnvironment.playerOffScreenPosition.position, 1.0f).SetEase(Ease.InQuad).OnComplete(() =>
                                    {
                                        AttackDrone[] drones = GameObject.FindObjectsOfType<AttackDrone>();
                                        foreach (AttackDrone d in drones)
                                        {
                                            d.TakeDamage(1000000f);
                                        }
                                        boss.gameObject.SetActive(false);
                                    });
                }
            });

            UserInformation.Instance.win = false;
            UserInformation.Instance.timetaken = stateMachine.gameEnvironment.StepCounter;
            UserInformation.Instance.score = stateMachine.gameEnvironment.scoreCounter.Score;
            stateMachine.gameEnvironment.scoreCounter.canAddScore = false;
            UserInformation.Instance.SendData();
            stateMachine.gameOverSign.SetActive(true);
            stateMachine.gameOverSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            stateMachine.gameOverSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f).OnComplete(() =>
            {
                stateMachine.gameOverSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 3f).OnComplete(() =>
                {
                    stateMachine.gameOverSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 0f), 0.5f).OnComplete(() =>
                    {
                        showLeaderboard = true;
                    });
                });
            });
        }

        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if (timer >= 2.0f)
            {
                stateMachine.parallaxController.backgroundSpeed = stateMachine.parallaxController.backgroundSpeed > 0 ? stateMachine.parallaxController.backgroundSpeed - Time.deltaTime * 4.0f : 0;
                if (showLeaderboard)
                {
                    showLeaderboard = false;
                    stateMachine.retryButton.SetActive(true);
                    stateMachine.gameModesButton.SetActive(true);
                    stateMachine.leaderboard.ShowLeaderBoard();
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

}
