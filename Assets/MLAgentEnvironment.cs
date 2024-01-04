using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class MLAgentEnvironment : MonoBehaviour{
    private List<GameObject> spawnedObjects = new List<GameObject>();

    public void AddObject(GameObject obj){
        spawnedObjects.Add(obj);
    }

    public void RemoveSpawnedObjects(){
        foreach(GameObject obj in spawnedObjects){
            if(obj != null){
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
    }
}
