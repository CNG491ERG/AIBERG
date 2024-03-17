using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : InputHandler{
    private void FixedUpdate() {
        this.BasicAbilityInput = Input.GetMouseButton(0);
        this.JumpInput = Input.GetKey(KeyCode.Space);
        this.ActiveAbility1Input = Input.GetKey(KeyCode.Q);
        this.ActiveAbility2Input = Input.GetKey(KeyCode.E);
    }
}
