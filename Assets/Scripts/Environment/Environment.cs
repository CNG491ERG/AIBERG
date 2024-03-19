using System;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour{
    [Header("Step Information")]
    [SerializeField] private const int maxSteps = 6000;
    [SerializeField] private long stepCounter;

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform bossSpawnPosition;
    [SerializeField] private List<GameObject> foregroundObjects;
    [SerializeField] private List<GameObject> childObjects = new();

    public long StepCounter{get => stepCounter; private set => stepCounter = value;}
    public Player Player{get => player; private set => player = value;}
    public Boss Boss{get => boss; private set => boss = value;}
    public List<GameObject> ChildObjects {get => childObjects; private set => childObjects = value;}

    public event EventHandler OnGameEnded;
    
    private void Awake() {
        player = Utility.ComponentFinder.FindComponentInChildren<Player>(this.transform);
        boss = Utility.ComponentFinder.FindComponentInChildren<Boss>(this.transform);
        foregroundObjects = Utility.ComponentFinder.FindGameObjectsWithTagInChildren("ForegroundObject", this.transform);
    }

    private void Start() {
        stepCounter = 0;
        OnGameEnded += Environment_OnGameEnded;
        player.GetComponent<IDamageable>().OnDamageableDeath += Environment_OnGameEnded;
        boss.GetComponent<IDamageable>().OnDamageableDeath += Environment_OnGameEnded;
    }

    private void Environment_OnGameEnded(object sender, EventArgs e){
        ResetEnvironment();
    }

    private void FixedUpdate() {
        stepCounter++;
        if(stepCounter == maxSteps){
            OnGameEnded?.Invoke(this, EventArgs.Empty);
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
