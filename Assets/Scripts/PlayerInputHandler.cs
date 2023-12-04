using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;

    private void Awake() {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }

    private void FixedUpdate() {
        bool basicAbilityInput = Input.GetMouseButton(0);
        bool jumpInput = Input.GetKey(KeyCode.Space);
        
        if(basicAbilityInput){
            eventViewer.eventsBeingPerformed.Add(faction.basicAttack.abilityName);
        }
        if(jumpInput){
            eventViewer.eventsBeingPerformed.Add(faction.jumpAbility.abilityName);
        }
        
        faction.basicAttack.UseAbility(basicAbilityInput);
        faction.jumpAbility.UseAbility(jumpInput);
    }
}
