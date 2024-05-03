using System;
using System.Collections;
using System.Collections.Generic;
using AIBERG.Core;
using AIBERG.ParkourMode;
using UnityEngine;

namespace AIBERG.ParkourMode.States{
public class GameStateMachineScript : MonoBehaviour
{
    public BaseState currentState;
    public BossFightState BossFight = new BossFightState();
    public BossFightToParkourState BossFightToParkour = new BossFightToParkourState();
    public ParkourToBossFightState ParkourToBossFight = new ParkourToBossFightState();
    public ParkourState Parkour = new ParkourState();
    public GameOverState GameOver = new GameOverState();
    public InitialState initialState = new InitialState();
    public GameObject spawnPoint;
    [SerializeField] protected GameEnvironment environment;
    [SerializeField] protected Player player;
    [SerializeField] protected Boss boss;

        public GameEnvironment GetEnvironment()
        {
            return environment;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public Boss GetBoss()
        {
            return boss;
        }

        // Start is called before the first frame update
        void Start() {
            player = GameObject.Find("Player_Game").GetComponent<Player>();
            boss = GameObject.Find("Boss_GameAgent").GetComponent<Boss>();
            spawnPoint = GameObject.Find("SpawnPoint");

            if (spawnPoint != null) {
                if (initialState != null) {
                    currentState = initialState;
                    currentState.EnterState(this);
                }
                else {
                    Debug.LogError("Initial state not assigned!");
                }
            }
            else {
                Debug.LogError("Spawn point not found!");
            }
        }

        //MIGHT NEED TO CHANGE THIS INTO FIXEDUPATE IN FUTURE
        void Update() {
            if (currentState != null) {
                currentState.UpdateState(this);
            }
            else {
                Debug.LogError("currentState is null in update!");
            }
        }

        public void SwitchState(BaseState state) {
            currentState = state;
            state.EnterState(this);
        }
}

}
