using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour, IBossAbility{
    [SerializeField] private Boss boss;
    [SerializeField] private Rigidbody2D bossRb;

    public string AbilityName => "MoveDown";

    public GameObject AbilityOwner => boss.gameObject;
    
    public float Cooldown => 0;

    public float AbilityDuration => 0;

    public bool CanBeUsed => true;

    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;

    private void Start() {
        boss = GetComponent<Boss>();
        bossRb = GetComponent<Rigidbody2D>();
        AbilityLock = this;
    }

    public void UseAbility(bool inputReceived){
        if(inputReceived){
            if(bossRb.velocity.y > -boss.speed){
                bossRb.AddForce(new Vector2(0, -boss.speed));
            }
        }
    }

    public void ResetCooldown(){
        return;
    }
}
