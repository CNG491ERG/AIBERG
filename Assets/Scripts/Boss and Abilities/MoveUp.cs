using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : BaseAbility{
    [SerializeField] private Boss boss;
    [SerializeField] private Rigidbody2D bossRb;
    private void Start() {
        boss = GetComponent<Boss>();
        bossRb = GetComponent<Rigidbody2D>();
        this.abilityName = "MoveUp";
        this.abilityOwner = boss.gameObject;
    }


    public override void UseAbility(bool inputReceived){
        if(inputReceived){
            if(bossRb.velocity.y < boss.speed){
                bossRb.AddForce(new Vector2(0, boss.speed));
            }
        }
    }

}
