using System.Linq;
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

            float endPoint = stateManager.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y+1;
            player.transform.DOLocalMoveY(endPoint, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>{
                boss.transform.DOLocalMove(stateManager.gameEnvironment.playerOffScreenPosition.position, 1.0f).SetEase(Ease.InQuad).OnComplete(() => {
                    allMovementComplete = true;
                });
            });
        }

        public override void UpdateState(BossModeStateManager stateManager){
            if(allMovementComplete){
                Debug.Log("Boss mode is complete - player died, do whatever is next!");
            }
        }
    }
}
