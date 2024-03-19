using UnityEngine;

public class Despawner : MonoBehaviour{
    Environment environment;
    private void Start() {
        environment = Utility.ComponentFinder.FindComponentInParents<Environment>(this.transform);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DespawnObject(other.gameObject);
    }
    private void DespawnObject(GameObject g){
        if(environment.ChildObjects.Contains(g)){
            Destroy(g);
        }
    }
}
