using AIBERG.BossAbilities;
using AIBERG.Core;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.ParkourMode.States
{
    public class BossFightToParkourState : BaseState
    {
        private Boss boss;

        float timer;
        public override void EnterState(GameStateMachineScript stateMachine)
        {
            boss = stateMachine.gameEnvironment.Boss;
            timer = 0;
            boss.transform.DOLocalMove(stateMachine.gameEnvironment.bossOffScreenPosition.position, 3.0f).SetEase(Ease.InBack).OnComplete(() =>
            {
                AttackDrone[] drones = GameObject.FindObjectsOfType<AttackDrone>();
                foreach (AttackDrone d in drones)
                {
                    d.TakeDamage(1000000f);
                }
                GameObject.Destroy(boss.gameObject);
            });

            stateMachine.bossDefeatedSign.gameObject.SetActive(true);
            stateMachine.bossDefeatedSign.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            stateMachine.bossDefeatedSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 0.5f).OnComplete(() =>
            {
                stateMachine.bossDefeatedSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1f), 3f).OnComplete(() =>
                {
                    stateMachine.bossDefeatedSign.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 0f), 0.5f);
                });
            });
        }


        public override void UpdateState(GameStateMachineScript stateManager)
        {
            if (timer >= 2.0f)
            {
                stateManager.SwitchState(stateManager.Parkour);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}

