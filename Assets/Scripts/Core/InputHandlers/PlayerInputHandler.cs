using UnityEngine;

namespace AIBERG.Core.InputHandlers{
public class PlayerInputHandler : InputHandler{
    private void FixedUpdate() {
        this.BasicAbilityInput = Input.GetMouseButton(0);
        this.JumpInput = Input.GetKey(KeyCode.Space);
        this.ActiveAbility1Input = Input.GetKey(KeyCode.Q);
        this.ActiveAbility2Input = Input.GetKey(KeyCode.E);
    }
    private void OnDisable() {
        this.BasicAbilityInput = false;
        this.JumpInput = false;
        this.ActiveAbility1Input = false;
        this.ActiveAbility2Input = false;
    }
}
}