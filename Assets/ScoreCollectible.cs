using AIBERG.Core;
using AIBERG.ParkourMode.States;
using UnityEngine;

namespace AIBERG
{
    public class ScoreCollectible : Collectible
    {
        public long scoreToAdd;
        private Rigidbody2D rb;
        private GameEnvironment environment;
        [SerializeField] public AudioClip pickupSound;
        [SerializeField] public AudioSource pickupSoundAudioSource;
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
                SoundManager.Instance.PlaySound(pickupSoundAudioSource, pickupSound, 1.0f, 2.0f);
                var parkourStateMachine = Utilities.ComponentFinder.FindComponentInChildren<GameStateMachineScript>(environment.transform);
                long scoreMultiplier = parkourStateMachine.Parkour.parkourStateCounter * parkourStateMachine.Parkour.parkourStateCounter;
                environment.scoreCounter.AddScore(scoreToAdd * scoreMultiplier);
                Destroy(this.gameObject);
            }
        }
    }
}
