using System;
using System.Collections.Generic;
using UnityEngine;
using AIBERG.Utilities;


namespace AIBERG.Core{
    public class GameEnvironment : MonoBehaviour{
    [Header("Step Information")]
    [SerializeField] private const int maxSteps = 6000;
    [SerializeField] private long stepCounter;
    [SerializeField] private bool countStep = false;
    [SerializeField] private bool isTrainingEnvironment = true;

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform bossSpawnPosition;
    [SerializeField] public Transform playerOffScreenPosition;
    [SerializeField] public Transform bossOffScreenPosition;
    [SerializeField] private List<GameObject> foregroundObjects;
    [SerializeField] private List<GameObject> childObjects = new();
    
    [Header("Automatic Agent Test")]
    [SerializeField] private bool countMatches = false;
    [SerializeField] private int totalMatches = 4;
    [SerializeField] private int currentMatch = 0;

    public long StepCounter{get => stepCounter; private set => stepCounter = value;}
    public Player Player{get => player;  set => player = value;}
    public Boss Boss{get => boss;  set => boss = value;}
    public List<GameObject> ChildObjects {get => childObjects; private set => childObjects = value;}
    public List<GameObject> ForegroundObjects {get => foregroundObjects; private set => foregroundObjects = value;}
    public Transform PlayerSpawnPosition {get => playerSpawnPosition; private set => playerSpawnPosition = value;}
    public Transform BossSpawnPosition {get => bossSpawnPosition; private set => bossSpawnPosition = value;}
    public long MaxSteps{get => maxSteps;}
    public bool IsTrainingEnvironment{get => isTrainingEnvironment; set => isTrainingEnvironment = value;}
    public event EventHandler OnMaxStepsReached;
    public void Awake() {
        if(player == null){
            player = ComponentFinder.FindComponentInChildren<Player>(this.transform);
        }
        if(boss == null){
            boss = ComponentFinder.FindComponentInChildren<Boss>(this.transform);
        }
        foregroundObjects = ComponentFinder.FindGameObjectsWithTagInChildren("ForegroundObject", this.transform);
    }

    public void Start() {
        stepCounter = 0;
    }

    private void FixedUpdate() {
        if(countStep){
            stepCounter++;
        }
        if(stepCounter == maxSteps){
            OnMaxStepsReached?.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddObjectToEnvironmentList(GameObject obj){
        if(!childObjects.Contains(obj)){
            childObjects.Add(obj);
        }
    }

    private void RemoveSpawnedObjects(){
        foreach(GameObject obj in childObjects){
            if(obj != null){
                Destroy(obj);
            }
        }
        childObjects.Clear();
    }
    
    public void ResetEnvironment(){
        if (countMatches)
        {
            currentMatch++;
            
            if (currentMatch >= totalMatches) {
                Terminate();
                return;
            }   
        }
        
        RemoveSpawnedObjects();
        ResetPlayer();
        ResetBoss();
        stepCounter = 0;
    }

    private void ResetBoss(){
        boss.ResetAllCooldowns();
        boss.Health = boss.MaxHealth;
        boss.transform.localPosition = bossSpawnPosition.transform.localPosition;
    }

    private void ResetPlayer(){
        player.ResetAllCooldowns();
        player.Health = player.MaxHealth;
        player.transform.localPosition = playerSpawnPosition.transform.localPosition;
    }

    public void StartCountingSteps(){
        countStep = true;
    }

    public void StopCountingSteps(){
        countStep = false;
    }
    
    public void StartCountingMatches(){
        countMatches = true;
    }

    public void StopCountingMatches(){
        countMatches = false;
    }

    private void Terminate()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}

}
