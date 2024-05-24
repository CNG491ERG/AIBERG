using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG
{
    public class ScoreCollectible : MonoBehaviour, ICollectible
    {
        [SerializeField] private long score;
        public void Collect(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                player.Environment.scoreCounter.AddScore(score);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Collect(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Collect(other.collider);
        }
    }
}
