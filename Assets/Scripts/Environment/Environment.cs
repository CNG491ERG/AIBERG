using System;
using System.Collections.Generic;
using UnityEngine;


namespace AIBERG.Environment{
    public class Environment : MonoBehaviour{
    [Header("Step Information")]
    [SerializeField] private const int maxSteps = 6000;
    [SerializeField] private long stepCounter;

    [Header("References")]
    [SerializeField] private AIBERG.Player.Player player;
    [SerializeField] private AIBERG.Boss.Boss boss;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform bossSpawnPosition;
    [SerializeField] private List<GameObject> foregroundObjects;
    [SerializeField] private List<GameObject> childObjects = new();

    public long StepCounter{get => stepCounter; private set => stepCounter = value;}
    public AIBERG.Player.Player Player{get => player; private set => player = value;}
    public AIBERG.Boss.Boss Boss{get => boss; private set => boss = value;}
    public List<GameObject> ChildObjects {get => childObjects; private set => childObjects = value;}
    public List<GameObject> ForegroundObjects {get => foregroundObjects; private set => foregroundObjects = value;}
    public Transform PlayerSpawnPosition {get => playerSpawnPosition; private set => playerSpawnPosition = value;}
    public Transform BossSpawnPosition {get => bossSpawnPosition; private set => bossSpawnPosition = value;}
    public event EventHandler OnMaxStepsReached;
    private void Awake() {
        player = Utility.ComponentFinder.FindComponentInChildren<AIBERG.Player.Player>(this.transform);
        boss = Utility.ComponentFinder.FindComponentInChildren<AIBERG.Boss.Boss>(this.transform);
        foregroundObjects = Utility.ComponentFinder.FindGameObjectsWithTagInChildren("ForegroundObject", this.transform);
    }

    private void Start() {
        stepCounter = 0;
    }

    private void FixedUpdate() {
        stepCounter++;
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
}

}
