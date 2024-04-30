using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeStateManager : MonoBehaviour{
        BossModeBaseState currentState;
        BossModeInitialState initialState = new BossModeInitialState();

        void Start(){
            currentState = initialState;
            currentState.EnterState(this);
        }

        void Update(){
            currentState.UpdateState(this);
        }

        public void SwitchState(BossModeBaseState nextState){
            currentState = nextState;
            currentState.EnterState(this);
        }
    }
}
