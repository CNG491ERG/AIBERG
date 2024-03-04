using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour{
    private const int maxSteps = 6000;
    private Int64 stepCounter;
    private Int64 score;
    public static GameManager Instance;
    public static bool playerWon = false;

    public Int64 StepCount => stepCounter;
    public Int64 MaxSteps => maxSteps;
    public Int64 Score{
        get => score;
        set => score = value;
    }

    public Player player;
    public Boss boss;
    public bool isTraining = false;
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
        if(!isTraining){
            score = 0;
            player = FindFirstObjectByType<Player>();
            boss = FindFirstObjectByType<Boss>();
            player.GetComponent<IDamageable>().OnDamageableDeath += Player_OnDamageableDeath;
            boss.GetComponent<IDamageable>().OnDamageableDeath += Boss_OnDamageAbleDeath;
            OnGameStarted += GameManager_OnGameStarted;
            OnGameEnded += GameManager_OnGameEnded;
        }
    }

    private void Boss_OnDamageAbleDeath(object sender, EventArgs e){
        playerWon = true;
        OnGameEnded?.Invoke(this, EventArgs.Empty);
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e){
        playerWon = false;
        OnGameEnded?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate() {
        if(!isTraining){
            if(stepCounter == 1){
                OnGameStarted?.Invoke(this, EventArgs.Empty);
            }
            if(stepCounter == maxSteps){ //Game will end at 3 minute mark *exactly*
                OnGameEnded?.Invoke(this, EventArgs.Empty);
                playerWon = true;
            }
        }
        stepCounter++;
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e){
        //stepCounter = 0;
        score = 0;
        Debug.Log("Game Started");
    }

    private void GameManager_OnGameEnded(object sender, EventArgs e){
        //Time.timeScale = 0; //Pauses the game completely
        Debug.Log("Game Ended");
    }

    public void ResetStepCounter(){
        stepCounter = 0;
    }
}
