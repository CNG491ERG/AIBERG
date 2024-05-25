using UnityEngine;
using AIBERG.UI;
using AIBERG.Core;
using DG.Tweening;
using System.Data;

namespace AIBERG.ParkourMode.States
{
    public class ParkourToBossFightState : BaseState
    {
        private float stateTimer;
        private float stateDuration = 2.0f;
        private Player player;
        private GameObject bossGameObject;
        private Boss boss;
        private bool showDangerSign;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            bossGameObject = GameObject.Instantiate(stateMachine.bossPrefab, stateMachine.gameEnvironment.transform);
            bossGameObject.transform.localPosition = stateMachine.gameEnvironment.bossOffScreenPosition.localPosition;
            showDangerSign = true;
            player = stateMachine.gameEnvironment.Player;
            stateTimer = 0f;
            stateMachine.obstacleSpawner.StopSpawningObstacles();
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if (showDangerSign)
            {
                boss = bossGameObject.GetComponent<Boss>();
                boss.GetComponent<Collider2D>().enabled = false;
                boss.transform.DOLocalMove(stateMachine.gameEnvironment.BossSpawnPosition.localPosition, 1.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    boss.GetComponent<Collider2D>().enabled = true;
                });
                stateMachine.gameEnvironment.Boss = boss;
                boss.GetComponent<BossAgent>().boss = boss;
                boss.GetComponent<BossAgent>().player = player;
                stateMachine.dangerSign.SetActive(true);
                stateMachine.dangerSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);
                stateMachine.dangerSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
                showDangerSign = false;
            }
            if (stateTimer >= stateDuration)
            {
                stateMachine.SwitchState(stateMachine.BossFight);
            }
            else
            {
                stateTimer += Time.deltaTime;
            }
        }

    }
}

