using UnityEngine;
using AIBERG.UI;

namespace AIBERG.ParkourMode.States
{
    public class ParkourToBossFightState : BaseState
    {
        TextManager t;

        public override void EnterState(GameStateMachineScript stateMachine)
        {
            stateStartTime = Time.time;                                     // time at entering the state
            Debug.Log("Parkour to Boss");
            //t.TextEdit("Boss is Coming!!");                                 //calling fucntion from TextManager to edit the text to say Boss is Coming!!!
            ObstacleSpawner obstacleSpawner = stateMachine.spawnPoint.GetComponent<ObstacleSpawner>();
            if (obstacleSpawner != null)
            {
                obstacleSpawner.canSpawn = false; // Set canSpawn to false
            }

            GameObject[] obstacles;

            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");     //Array of Obstacle1 type objects

            foreach (GameObject obstacle in obstacles)                      //for each Obstacle1 type object
            {
                UnityEngine.Object.Destroy(obstacle);                       //destroy
            }
        }
        public override void UpdateState(GameStateMachineScript stateMachine)
        {
            if ((Time.time - stateStartTime) >= 2.0f)
            {
                Debug.Log(Time.time - stateStartTime);
                if (stateMachine.BossFight != null)
                {
                    stateMachine.SwitchState(stateMachine.BossFight);
                }
                else
                {
                    Debug.LogError("BossFight state is null!");
                }
            }
        }

    }
}

