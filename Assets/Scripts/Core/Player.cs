using System;
using AIBERG.Core.InputHandlers;
using AIBERG.Interfaces;
using AIBERG.ScriptableObjects;
using AIBERG.Utilities;
using DG.Tweening;
using UnityEngine;

namespace AIBERG.Core
{
    public class Player : MonoBehaviour, IDamageable
    {
        [Header("Animators")]
        public Animator armsAnimator;
        public Animator bodyAnimator;
        public Animator thrustersAnimator;

        [Header("Player's Input Handler")]
        public InputHandler inputHandler;

        [Header("Environment")]
        [SerializeField] private GameEnvironment environment;

        [Header("Faction Data of the Player")]
        [SerializeField] private FactionSO faction;
        [SerializeField] private GameObject basicAbilityObject;
        [SerializeField] private GameObject activeAbility1Object;
        [SerializeField] private GameObject activeAbility2Object;
        [SerializeField] private GameObject passiveAbilityObject;
        [SerializeField] private GameObject jumpObject;
        public IAbility basicAbility;
        public IAbility activeAbility1;
        public IAbility activeAbility2;
        public IAbility passiveAbility;
        public IAbility jump;

        [Header("IDamageable Values")]
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;
        [SerializeField] private float defense;

        public GameEnvironment Environment { get => environment; private set => environment = value; }
        public float Health { get => health; set => health = value; }
        public float Defense { get => defense; set => defense = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }


        [Header("Other")]
        [SerializeField] private Boss boss;
        [SerializeField] public Transform shootPoint;
        public event EventHandler OnDamageableDeath;
        public event EventHandler OnDamageableHurt;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
        }
        void Start()
        {
            environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
            if (faction.BasicAbility != null)
            {
                basicAbilityObject = Instantiate(faction.BasicAbility, this.transform);
                basicAbility = basicAbilityObject.GetComponent<IAbility>();
            }
            if (faction.ActiveAbility1 != null)
            {
                activeAbility1Object = Instantiate(faction.ActiveAbility1, this.transform);
                activeAbility1 = activeAbility1Object.GetComponent<IAbility>();
            }
            if (faction.ActiveAbility2 != null)
            {
                activeAbility2Object = Instantiate(faction.ActiveAbility2, this.transform);
                activeAbility2 = activeAbility2Object.GetComponent<IAbility>();
            }
            if (faction.PassiveAbility != null)
            {
                passiveAbilityObject = Instantiate(faction.PassiveAbility, this.transform);
                passiveAbility = passiveAbilityObject.GetComponent<IAbility>();
            }
            if (faction.Jump != null)
            {
                jumpObject = Instantiate(faction.Jump, this.transform);
                jump = jumpObject.GetComponent<IAbility>();
            }

            health = maxHealth = faction.MaxHealth;
            defense = faction.Defense;
        }

        private void FixedUpdate()
        {
            if (this.GetComponent<PlayerAgent>() == null)
            {
                basicAbility?.UseAbility(inputHandler.BasicAbilityInput);
                activeAbility1?.UseAbility(inputHandler.ActiveAbility1Input);
                activeAbility2?.UseAbility(inputHandler.ActiveAbility2Input);
                jump?.UseAbility(inputHandler.JumpInput);
            }
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.down, 0.75f);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 1f), Vector2.down);
            bodyAnimator.SetBool("isOnGround", hit.collider != null && hit.collider.tag.CompareTo("ForegroundObject") == 0);
        }

        bool isDead = false;
        public void TakeDamage(float damageToTake)
        {
            float totalDamage = damageToTake * (1 - Defense);
            Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
            if (damageToTake > 0)
            {
                armsAnimator.GetComponent<SpriteRenderer>().material.color = Color.red;
                armsAnimator.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 0.2f);
                bodyAnimator.GetComponent<SpriteRenderer>().material.color = Color.red;
                bodyAnimator.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 0.2f);
            }
            else if (damageToTake < 0)
            {
                armsAnimator.GetComponent<SpriteRenderer>().material.color = Color.green;
                armsAnimator.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 0.2f);
                bodyAnimator.GetComponent<SpriteRenderer>().material.color = Color.green;
                bodyAnimator.GetComponent<SpriteRenderer>().material.DOColor(Color.white, 0.2f);
            }

            OnDamageableHurt?.Invoke(this, EventArgs.Empty);

            if (Health == 0)
            {
                if (!isDead)
                {
                    basicAbilityObject.SetActive(false);
                    activeAbility1Object.SetActive(false);
                    activeAbility2Object.SetActive(false);
                    jumpObject.SetActive(false);
                    armsAnimator.SetTrigger("death");
                    bodyAnimator.SetTrigger("death");
                    thrustersAnimator.SetTrigger("death");
                    OnDamageableDeath?.Invoke(this, EventArgs.Empty);
                    Debug.Log("Player died");
                    isDead = true;
                }
            }
        }

        public void ResetAllCooldowns()
        {
            basicAbility?.ResetCooldown();
            activeAbility1?.ResetCooldown();
            activeAbility2?.ResetCooldown();
            jump?.ResetCooldown();
            passiveAbility?.ResetCooldown();
        }
    }

}
