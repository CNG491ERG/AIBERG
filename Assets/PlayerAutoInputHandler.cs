using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;
    [SerializeField] private Boss boss;
    private void Start() {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }


    private void FixedUpdate() {
        bool basicAbilityInput = true; //Always use basic input as it has no cooldown
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius: 10f, transform.right);
        bool jumpInput = false;
        if(hit.collider != null){
            if(hit.collider.GetComponent<DamagingProjectile>() != null){
                if(hit.collider.GetComponent<Rigidbody2D>().velocity.x < 0){
                    jumpInput = true;
                }
                else{
                    jumpInput = false;
                }
            }
        }
    
        bool activeAbility1Input = faction.ActiveAbility1.CanBeUsed;
        bool activeAbility2Input = faction.ActiveAbility2.CanBeUsed;
        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.JumpAbility.UseAbility(jumpInput);
        faction.ActiveAbility1.UseAbility(activeAbility1Input);
        faction.ActiveAbility2.UseAbility(activeAbility2Input);
    }
}
