using System.Collections;
using System.Collections.Generic;
using AIBERG.Core;
using AIBERG.GameStateMachines;
using UnityEngine;

namespace AIBERG.GameStateMachines{
public class GameStateMachineScript : MonoBehaviour
{
    public BaseState currentState;
    public BossFightState BossFight;
    public BossFightToParkourState BossFightToParkour = new BossFightToParkourState();
    public ParkourToBossFightState ParkourToBossFight = new ParkourToBossFightState();
    public ParkourState Parkour = new ParkourState();
    public GameOverState GameOver = new GameOverState();
    public InitialState initialState = new InitialState();
    public GameObject SpawnPoint;

        // Start is called before the first frame update
        void Start() {

            GameObject spawnPoint = GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>();

            currentState = initialState;
        currentState.EnterState(this);
    }

    //MIGHT NEED TO CHANGE THIS INTO FIXEDUPATE IN FUTURE
    void Update()  {
        currentState.UpdateState(this);
    }
    //for switching states
    public void SwitchState(BaseState state) {
        currentState = state;
        state.EnterState(this);
    }
}

}
