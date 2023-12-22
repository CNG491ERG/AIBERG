using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour, IBossAbility{
    [SerializeField] private Boss boss;
    [SerializeField] private Rigidbody2D bossRb;

    public string AbilityName => "MoveUp";

    public GameObject AbilityOwner => boss.gameObject;
    
    public float Cooldown => 0;

    public float AbilityDuration => 0;

    public bool CanBeUsed => true;

    private void Start() {
        boss = GetComponent<Boss>();
        bossRb = GetComponent<Rigidbody2D>();
    }


    public void UseAbility(bool inputReceived){
        if(inputReceived){
            if(bossRb.velocity.y < boss.speed){
                bossRb.AddForce(new Vector2(0, boss.speed));
            }
        }
    }

    public void ResetCooldown(){
        return;
    }
}
