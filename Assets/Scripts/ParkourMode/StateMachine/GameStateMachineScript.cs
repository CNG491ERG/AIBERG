using AIBERG.Core;
using AIBERG.Utilities;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace AIBERG.ParkourMode.States
{
    public class GameStateMachineScript : MonoBehaviour
    {
        BaseState currentState;
        public BossFightState BossFight = new BossFightState();
        public BossFightToParkourState BossFightToParkour = new BossFightToParkourState();
        public ParkourToBossFightState ParkourToBossFight = new ParkourToBossFightState();
        public ParkourState Parkour = new ParkourState();
        public GameOverState GameOver = new GameOverState();
        public InitialState initialState = new InitialState();
        public GameEnvironment gameEnvironment;
        public ObstacleSpawner obstacleSpawner;
        public GameObject dangerSign;
        public GameObject gameOverSign;
        public GameObject bossDefeatedSign;
        public GameObject bossPrefab;
        public Leaderboard leaderboard;
        public GameObject retryButton;
        public GameObject gameModesButton;
        public ParallaxController parallaxController;
        
        void Start()
        {
            gameEnvironment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
            gameEnvironment.IsTrainingEnvironment = false;
            currentState = initialState;
            currentState.EnterState(this);
        }

        //MIGHT NEED TO CHANGE THIS INTO FIXEDUPATE IN FUTURE
        void Update()
        {
            currentState.UpdateState(this);
        }

        public void SwitchState(BaseState nextState)
        {
            currentState = nextState;
            nextState.EnterState(this);
        }
    }

}
