using AIBERG.API;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeInitialState : BossModeBaseState
    {
        private float stateTimer;
        private float stateDuration = 2.0f;
        private Player player;
        private Boss boss;
        private bool startMoving;
        public override void EnterState(BossModeStateManager stateManager)
        {
            stateManager.gameEnvironment.StopCountingSteps();
            stateManager.gameEnvironment.scoreCounter.canAddScore = true;
            startMoving = true;
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;
            UserInformation.Instance.ResetUserInformation();
            UserInformation.Instance.playMode = true;
            player.inputHandler.enabled = false;
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            stateTimer = 0f;
        }

        public override void UpdateState(BossModeStateManager stateManager)
        {
            if (startMoving)
            {
                player.transform.position = stateManager.gameEnvironment.playerOffScreenPosition.position;
                boss.transform.position = stateManager.gameEnvironment.bossOffScreenPosition.position;
                player.transform.DOLocalMove(stateManager.gameEnvironment.PlayerSpawnPosition.localPosition, 1.5f).SetEase(Ease.OutQuad);
                boss.transform.DOLocalMove(stateManager.gameEnvironment.BossSpawnPosition.localPosition, 1.5f).SetEase(Ease.OutQuad);
                stateManager.dangerSign.SetActive(true);
                stateManager.dangerSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0);
                stateManager.dangerSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f);
                startMoving = false;
            }
            if (stateTimer >= stateDuration)
            {
                stateManager.SwitchState(stateManager.bossFightState); //Switch to boss fight state
            }
            else
            {
                stateTimer += Time.deltaTime;
            }
            if (player.bodyAnimator != null)
            {
                player.bodyAnimator.SetBool("isFlying", true);
            }
            if (player.thrustersAnimator != null)
            {
                player.thrustersAnimator.SetBool("isFlying", true);
            }
        }
    }
}
