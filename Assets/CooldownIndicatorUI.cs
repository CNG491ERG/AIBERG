using AIBERG.Core;
using UnityEngine;
using UnityEngine.UI;

namespace AIBERG
{
    public class CooldownIndicatorUI : MonoBehaviour
    {
        public Image cooldownIcon;
        public GameEnvironment environment;
        public bool useAbility1;
        public bool useAbility2;
        public Animator animator;
        private void Start() {
            cooldownIcon = GetComponent<Image>();
            animator = GetComponent<Animator>();
        }
        void FixedUpdate()
        {
            if(useAbility1){
                cooldownIcon.fillAmount = environment.Player.activeAbility1.CooldownTimer / environment.Player.activeAbility1.Cooldown;
                animator.SetBool("canBeUsed", environment.Player.activeAbility1.CanBeUsed);
            }
            if(useAbility2){
                cooldownIcon.fillAmount = environment.Player.activeAbility2.CooldownTimer / environment.Player.activeAbility2.Cooldown;
                animator.SetBool("canBeUsed", environment.Player.activeAbility2.CanBeUsed);
            }
            
        }
    }
}
