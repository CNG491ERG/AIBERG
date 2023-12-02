using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingProjectile : MonoBehaviour{
    public float damage;
    public string tagToDamage;
    public Vector2 projectileVelocity;
    private Rigidbody2D rb;
    void Start(){
        GetComponent<Collider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        //Normally this wasn't needed, but once removed
        //projectiles shot by the boss behave unexpectedly
        rb.velocity = projectileVelocity;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable objectToDamage = other.gameObject.GetComponent<IDamageable>();
        //Not too good but needed for solving the problem where collider
        //spawning inside another collider registers as collision.
        if(objectToDamage != null && other.CompareTag(tagToDamage))
        { 
            Debug.Log("Projectile hit damageable object " + other.gameObject.name);
            objectToDamage.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        
    }
    private void OnDrawGizmos() {
        UnityEditor.Handles.Label(transform.position, "Velocity: " + rb.velocity.ToString());
    }
}
