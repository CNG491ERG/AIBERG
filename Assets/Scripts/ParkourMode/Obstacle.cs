using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG.ParkourMode
{
    /// <summary>
    /// Obstacle class for obstacles
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] public ParallaxController parallaxController;
        
        private void FixedUpdate()
        {
            if (parallaxController != null)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-parallaxController.backgroundSpeed / 1.5f, 0);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0f);
            }
        }
        
        /// <summary>
        /// OnTrigger enter 2d method for obstacle class
        /// </summary>
        /// <param name="collision"> whatever collides with this class</param>
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                var damagableComponent = collision.gameObject.GetComponent<Player>() as IDamageable;
                damagableComponent.TakeDamage(damagableComponent.MaxHealth * 0.3f);
            }
        }
    }

}
