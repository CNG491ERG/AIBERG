using AIBERG.Core;
using UnityEngine;
using AIBERG.API;

namespace AIBERG.BossMode
{
    public class BossModeStateManager : MonoBehaviour{
        BossModeBaseState currentState;
        public BossModeInitialState initialState = new BossModeInitialState();
        public BossModeBossFightState bossFightState = new BossModeBossFightState();
        public BossModeDeathState deathState = new BossModeDeathState();
        public BossModeGameOverState gameOverState= new BossModeGameOverState();
        public GameObject dangerSign;
        public GameObject gameOverSign;
        public Leaderboard leaderboard;
        public GameEnvironment gameEnvironment{get; private set;}
        public InputRecorder inputRecorder;
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
