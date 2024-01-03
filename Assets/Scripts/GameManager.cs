using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private Int64 stepCounter;
    private Int64 score;
    public static GameManager Instance;

    public Int64 StepCount => stepCounter;
    public Int64 Score{
        get => score;
        set => score = value;
    }

    public event EventHandler OnGameStarted;
    public event EventHandler OnGameEnded;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }

    private void Start() {
        OnGameStarted += GameManager_OnGameStarted;
        OnGameEnded += GameManager_OnGameEnded;
        OnGameStarted?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate() {
        if(stepCounter == 9000){ //Game will end at 3 minute mark *exactly*
            OnGameEnded?.Invoke(this, EventArgs.Empty);
        }
        stepCounter++;
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e){
        stepCounter = 0;
        score = 0;
        Debug.Log("Game Started");
    }

    private void GameManager_OnGameEnded(object sender, EventArgs e){
        Time.timeScale = 0; //Pauses the game completely
        Debug.Log("Game Ended");
    }

    public void ResetStepCounter(){
        stepCounter = 0;
    }
}
