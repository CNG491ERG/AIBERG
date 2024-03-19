using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    [SerializeField] private List<GameObject> foregroundObjects;
    [SerializeField] private List<GameObject> childObjects = new();
    public Player Player{get => player; private set => player = value;}
    public Boss Boss{get => boss; private set => boss = value;}
    

    private void Start() {
        player = Utility.ComponentFinder.FindComponentInChildren<Player>(this.transform);
        boss = Utility.ComponentFinder.FindComponentInChildren<Boss>(this.transform);
        foregroundObjects = Utility.ComponentFinder.FindGameObjectsWithTagInChildren("ForegroundObject", this.transform);
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
}
