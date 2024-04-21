using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    BaseState state;
    GameStateMachineScript stateMachine;

    private void Start()
    {
        stateMachine = FindObjectOfType<GameStateMachineScript>();
        if (stateMachine == null)
        {
            Debug.LogError("No GameStateMachine found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine != null && stateMachine.currentState != null)
        {
            int counter = stateMachine.currentState.GetParkourCounter();
            transform.position += new Vector3(cameraSpeed + (counter * Time.deltaTime), 0, 0);
        }
    }
}
