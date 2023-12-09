using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;

    private void Start() {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }

    private void FixedUpdate() {
        bool basicAbilityInput = Input.GetMouseButton(0);
        bool jumpInput = Input.GetKey(KeyCode.Space);
        bool activeAbility1Input = Input.GetKey(KeyCode.Q);
        bool activeAbility2Input = Input.GetKey(KeyCode.E);

        if(basicAbilityInput){
            eventViewer.eventsBeingPerformed.Add(faction.BasicAttack.AbilityName);
        }
        if(jumpInput){
            eventViewer.eventsBeingPerformed.Add(faction.JumpAbility.AbilityName);
        }
        if(activeAbility1Input){
            eventViewer.eventsBeingPerformed.Add(faction.ActiveAbility1.AbilityName);
        }
        if(activeAbility2Input){
            eventViewer.eventsBeingPerformed.Add(faction.ActiveAbility2.AbilityName);
        }
        
        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.JumpAbility.UseAbility(jumpInput);
        faction.ActiveAbility1.UseAbility(activeAbility1Input);
        faction.ActiveAbility2.UseAbility(activeAbility2Input);
        
    }
}
