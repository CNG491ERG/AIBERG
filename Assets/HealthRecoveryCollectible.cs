using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG
{
    public class HealthRecoveryCollectible : Collectible
    {
        public float healthToRecoverNormalized;
        private Rigidbody2D rb;
        private GameEnvironment environment;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            environment = Utilities.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
            environment.AddObjectToEnvironmentList(this.gameObject);
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(-7.5f, Mathf.Sin(Time.time * 7.5f) * 4);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = environment.Player;
                (environment.Player as IDamageable).TakeDamage(-(player.MaxHealth * healthToRecoverNormalized));
                Destroy(this.gameObject);
            }
        }
    }
}
