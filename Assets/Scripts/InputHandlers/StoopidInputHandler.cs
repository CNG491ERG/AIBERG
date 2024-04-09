using AIBERG.Core;
using UnityEngine;

namespace AIBERG.InputHandlers{
public class StoopidInputHandler : InputHandler{
    public Transform boss; // Reference to the boss GameObject

    // Distance threshold for considering alignment with the boss
    public float alignmentThreshold;

    private void Start() {
        boss = Utility.ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform).Boss.transform;
        this.BasicAbilityInput = true;
    }

    void FixedUpdate(){
        // Check if the player and boss are roughly aligned on the y-axis
        if (Mathf.Abs(transform.localPosition.y - boss.localPosition.y) < alignmentThreshold){
            this.ActiveAbility1Input = true;
            this.ActiveAbility2Input = true;
        }
        else{
            this.ActiveAbility1Input = false;
            this.ActiveAbility2Input = false;
        }

        if(transform.localPosition.y - boss.localPosition.y > alignmentThreshold){
            this.JumpInput = false;
        }
        if(transform.localPosition.y - boss.localPosition.y < -alignmentThreshold){
            this.JumpInput = true;
        }
    }

}

}