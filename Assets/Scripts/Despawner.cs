using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Anything colliding with despawner will be destroyed, except for the boss
//and the player
public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        DespawnObject(other.gameObject);
    }
    private void DespawnObject(GameObject g){
        if(!g.CompareTag("Player") && !g.CompareTag("Boss")){
            Destroy(g);
        }
    }
}
