using UnityEngine;

namespace AIBERG.InputHandlers{
public class InputHandler : MonoBehaviour{

    public bool ActiveAbility1Input{
        get;
        protected set;
    }
    public bool ActiveAbility2Input{
        get;
        protected set;
    }
    public bool BasicAbilityInput{
        get;
        protected set;
    }
    public bool JumpInput{
        get;
        protected set;
    }
}

}
