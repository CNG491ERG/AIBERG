using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG.Factions.TheOrderOfTheFlesh
{
    public class Jump : MonoBehaviour, IAbility
    {
        [Header("Player Reference")]
        [SerializeField] private Player player;
        [SerializeField] private Rigidbody2D playerRb;

        [Header("Ability Properties")]
        [SerializeField] private const bool canBeUsed = true;
        [SerializeField] private float jumpForce;

        #region interface properties
        public GameObject AbilityOwner => player.gameObject;
        public float Cooldown => 0;
        public float AbilityDuration => 0;
        public bool CanBeUsed => canBeUsed;
        public IAbility AbilityLock
        {
            get => abilityLock;
            set
            {
                if ((Object)value == (Object)this || value == null)
                {
                    abilityLock = value;
                }
            }
        }
        private IAbility abilityLock;
        #endregion

        private void Start()
        {
            player = Utilities.ComponentFinder.FindComponentInParents<Player>(this.transform);
            playerRb = player.GetComponent<Rigidbody2D>();
            AbilityLock = this;

        }

        public void UseAbility(bool inputReceived)
        {
            if (inputReceived)
            {
                playerRb.AddForce(new Vector2(0, jumpForce));
            }
            if (player != null)
            {
                if (player.bodyAnimator != null)
                {
                    player.bodyAnimator.SetBool("isFlying", playerRb.velocity.y > 0.001f);
                }
                if (player.thrustersAnimator != null)
                {
                    player.thrustersAnimator.SetBool("isFlying", playerRb.velocity.y > 0.001f);
                }
            }

        }

        public void ResetCooldown()
        {
            return;
        }
    }
}