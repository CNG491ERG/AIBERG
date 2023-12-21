using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IPlayerAbility, IAttackAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletVelocityX;
    [SerializeField] private float cooldownTimer;

    public Faction PlayerFaction => faction;

    public string AbilityName => "AssaultRifle";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 0.20f;

    public float AbilityDuration => 0;

    public float Damage => 0.25f;

    public bool CanBeUsed => true;

    private void Start() {
        this.faction = GetComponentInParent<Faction>();
        this.cooldownTimer = Cooldown;
        this.bulletPrefab.GetComponent<DamagingProjectile>().damage = Damage;
    }

    public void UseAbility(bool inputReceived){
        if(cooldownTimer>=(Cooldown-0.0001f) && inputReceived){ 
            Rigidbody2D bulletRigidBody = Instantiate(bulletPrefab).GetComponent<Rigidbody2D>();
            bulletRigidBody.transform.parent = this.transform;
            bulletRigidBody.transform.localPosition = Vector3.zero;
            bulletRigidBody.gameObject.GetComponent<DamagingProjectile>().projectileVelocity = new Vector2(bulletVelocityX, 0);
            cooldownTimer = 0;
        }
        cooldownTimer = cooldownTimer >= (Cooldown-0.001f) ? cooldownTimer : cooldownTimer + Time.fixedDeltaTime;
    }
}
