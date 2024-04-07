using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachineScript : MonoBehaviour
{
    public BaseState currentState;
    public BossFightState BossFight;
    public BossFightToParkourState BossFightToParkour = new BossFightToParkourState();
    public ParkourToBossFightState ParkourToBossFight = new ParkourToBossFightState();
    public ParkourState Parkour = new ParkourState();
    public GameOverState GameOver = new GameOverState();

    // Start is called before the first frame update
    void Start()
    {
        currentState = Parkour;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
