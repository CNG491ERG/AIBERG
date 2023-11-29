using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : BaseAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer = 0; 
    
    private void Start() {
        this.faction = GetComponentInParent<Faction>();
        this.abilityName = "AssaultRifle";
        this.abilityOwner = faction.player.gameObject;
        this.abilityType = AbilityType.BASIC;
        this.cooldown = 0.2f;
        this.damage = 0.25f;
        this.duration = 0;
        cooldownTimer = 0.2f;
    }

    // FixedUpdate called 50 times per second
    // (Every 0.02 seconds)
    void FixedUpdate(){
        if(Input.GetMouseButton(0)){  //TODO: This line must be moved to an InputH7andler
            UseAbility();
        }
        cooldownTimer = cooldownTimer >= (cooldown-0.001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }

    public override void UseAbility(){
        if(cooldownTimer>=(cooldown-0.001f)){ 
            Debug.Log("Using ability " + this.abilityName);
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab).GetComponent<Rigidbody2D>();
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.velocity = new Vector2(bulletVelocityX, 0);
            cooldownTimer = 0;
        }
    }
}
