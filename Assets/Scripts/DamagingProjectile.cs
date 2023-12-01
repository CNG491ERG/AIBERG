using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProjectile : MonoBehaviour{
    public float damage;
    void Start(){
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable objectToDamage = other.gameObject.GetComponent<IDamageable>();
        if(objectToDamage != null){
            Debug.Log("Projectile hit damageable object " + other.gameObject.name);
            objectToDamage.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        
    }
}
