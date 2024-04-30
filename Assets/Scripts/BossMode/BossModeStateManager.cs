using AIBERG.Core;
using UnityEngine;

namespace AIBERG.BossMode
{
    public class BossModeStateManager : MonoBehaviour{
        BossModeBaseState currentState;
        public BossModeInitialState initialState = new BossModeInitialState();
        public BossModeBossFightState bossFightState = new BossModeBossFightState();
        public BossModeDeathState deathState = new BossModeDeathState();
        public BossModeGameOverState gameOverState= new BossModeGameOverState();
        public GameEnvironment gameEnvironment{get; private set;}
        private void Awake() {
            gameEnvironment = Utilities.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform); 
            gameEnvironment.IsTrainingEnvironment = false;
        }
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
