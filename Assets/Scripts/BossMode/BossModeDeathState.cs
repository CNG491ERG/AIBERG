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
        private bool bossMovementComplete = false;
        public override void EnterState(BossModeStateManager stateManager)
        {
            player = stateManager.gameEnvironment.Player;
            boss = stateManager.gameEnvironment.Boss;  
            player.inputHandler.enabled = false;

            float endPoint = stateManager.gameEnvironment.ForegroundObjects.Where(t => t.gameObject.name == "Ground").First().transform.position.y+1;
            player.transform.DOLocalMoveY(endPoint, 1.5f).SetEase(Ease.OutBounce).OnComplete(() =>{
                boss.transform.DOLocalMove(stateManager.gameEnvironment.playerOffScreenPosition.position, 1.0f).SetEase(Ease.InQuad).OnComplete(() => {
                    bossMovementComplete = true;
                });

                //for whatever reason player starts to fly if the jump button was pressed at the time of this movement starts
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            });
        }

        public override void UpdateState(BossModeStateManager stateManager){
            if(bossMovementComplete){
                Debug.Log("Boss mode is complete, do whatever is next!");
            }
        }
    }
}
