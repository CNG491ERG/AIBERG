using AIBERG.Core;
using AIBERG.Interfaces;
using UnityEngine;

namespace AIBERG.BossAbilities{
    public class MoveUp : MonoBehaviour, IAbility{
    [Header("Boss Reference")]
    [SerializeField] private Boss boss;
    [SerializeField] private Rigidbody2D bossRb;

    [Header("Ability Properties")]
    [SerializeField] private const bool canBeUsed=true;
    [SerializeField] private float movementSpeed;

    #region interface properties
    public GameObject AbilityOwner => boss.gameObject;
    public float Cooldown => 0;
    public float AbilityDuration => 0;
    public bool CanBeUsed => canBeUsed;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;
    #endregion

    private void Start() {
        boss = Utilities.ComponentFinder.FindComponentInParents<Boss>(this.transform);
        movementSpeed = boss.Speed;
        AbilityLock = this;
    }

    public void UseAbility(bool inputReceived){
        if(inputReceived){
            bossRb = boss.GetComponent<Rigidbody2D>();
            bossRb.velocity = new Vector2(0, 2);
            if(bossRb.velocity.y < movementSpeed){
                bossRb.AddForce(new Vector2(0, movementSpeed));
            }
        }
    }

    public void ResetCooldown(){
        return;
    }
}

}
