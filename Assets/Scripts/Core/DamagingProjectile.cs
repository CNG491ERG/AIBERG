using UnityEngine;
using System.Collections.Generic;
using AIBERG.Interfaces;

namespace AIBERG.Core{
    public class DamagingProjectile : MonoBehaviour
{
    public float damage;
    public List<string> tagsToDamage;
    public Vector2 projectileVelocity;
    private Rigidbody2D rb;

    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = projectileVelocity;
    }

    public void AddTagToDamage(string tag){
        if(!tagsToDamage.Contains(tag)){
            tagsToDamage.Add(tag);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable objectToDamage = other.gameObject.GetComponent<IDamageable>();
        
        // Check if the collider's tag is in the list of tags to damage
        if (objectToDamage != null && tagsToDamage.Contains(other.tag))
        {
            objectToDamage.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

}
