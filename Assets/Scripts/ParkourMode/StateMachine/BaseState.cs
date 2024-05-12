using UnityEngine;
using AIBERG.Core;
using AIBERG.ParkourMode;

namespace AIBERG.ParkourMode.States
{
    public abstract class BaseState
    {
        protected static int parkourCounter = 0;
        protected static int bossCounter = 0;
        protected float stateStartTime;

        protected void ObstacleCleaner() {
            GameObject[] obstacles;

            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");     //Array of Obstacle1 type objects

            foreach (GameObject obstacle in obstacles)                      //for each Obstacle1 type object
            {
                UnityEngine.Object.Destroy(obstacle);                       //destroy
            }
        }

        protected void ObstacleHandler(GameStateMachineScript stateMachine, bool canSpawn)
        {
            if (stateMachine.spawnPoint != null)
            {
                ObstacleSpawner obstacleSpawner = stateMachine.spawnPoint.GetComponent<ObstacleSpawner>();
                if (obstacleSpawner != null)
                {
                    obstacleSpawner.canSpawn = canSpawn;
                }
            }
        }

        public int GetParkourCounter()
        {
            return parkourCounter;
        }
        public int GetBossCounter()
        {
            return bossCounter;
        }
        public abstract void EnterState(GameStateMachineScript stateMachine);
        public abstract void UpdateState(GameStateMachineScript stateMachine);
    }
}
