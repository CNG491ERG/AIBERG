using AIBERG.GameStateMachines;
using UnityEngine;

namespace AIBERG.Environment{
public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    BaseState state;
    [SerializeField] GameStateMachineScript stateMachine;

    private void Start()
    {
        /*stateMachine = FindObjectOfType<GameStateMachineScript>();
        if (stateMachine == null)
        {
            Debug.LogError("No GameStateMachine found in the scene!");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine != null && stateMachine.currentState != null)
        {
            int counter = stateMachine.currentState.GetParkourCounter();
            transform.position += new Vector3(cameraSpeed + (counter * Time.deltaTime), 0, 0);
        }
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}
}

