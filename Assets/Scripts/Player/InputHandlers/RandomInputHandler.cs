using UnityEngine;

public class RandomInputHandler : InputHandler{
    private void FixedUpdate() {
        this.JumpInput = Random.Range(0f,1f) < 0.15f;
        this.ActiveAbility1Input = Random.Range(0f,1f) < 0.01f;
        this.ActiveAbility2Input = Random.Range(0f,1f) < 0.01f;
        this.BasicAbilityInput = Random.Range(0f,1f) < 0.05f;
    }
}
