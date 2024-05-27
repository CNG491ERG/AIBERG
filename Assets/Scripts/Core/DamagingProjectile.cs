using UnityEngine;
using System.Collections.Generic;
using AIBERG.Interfaces;

namespace AIBERG.Core{
    public class DamagingProjectile : MonoBehaviour
{
    public float damage;
    public List<string> tagsToDamage = new List<string>();
    public Vector2 projectileVelocity;
    private Rigidbody2D rb;
    private bool hasImpacted = false;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if(!hasImpacted){
            rb.velocity = projectileVelocity;
        }
        else{
            rb.velocity = Vector2.zero;
        }
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
            SoundManager.Instance.PlaySound(audioSource, hitSound);
            hasImpacted = true;
            objectToDamage.TakeDamage(damage);
            GetComponentInChildren<Animator>().SetTrigger("impact");
        }
    }
}

}
