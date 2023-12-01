using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;

    private void Awake() {
        faction = transform.parent.GetComponentInChildren<Faction>();
    }

    private void FixedUpdate() {
        bool basicAbilityInput = Input.GetMouseButton(0);
        bool jumpInput = Input.GetKey(KeyCode.Space);

        faction.basicAttack.UseAbility(basicAbilityInput);
        faction.jumpAbility.UseAbility(jumpInput);
    }
}
