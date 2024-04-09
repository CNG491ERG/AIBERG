using UnityEngine;
using AIBERG.Utilities;

namespace AIBERG.Core{
    public class Despawner : MonoBehaviour{
    GameEnvironment environment;
    private void Start() {
        environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
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

}
