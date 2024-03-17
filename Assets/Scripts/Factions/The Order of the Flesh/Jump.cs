using UnityEngine;

public class Jump : MonoBehaviour, IAbility{
    [Header("Player Reference")]
    [SerializeField] private Player player;

    [Header("Ability Properties")]
    [SerializeField] private const bool canBeUsed=true;
    [SerializeField] private float jumpForce;

    #region interface properties
    public GameObject AbilityOwner => player.gameObject;
    public float Cooldown => 0;
    public float AbilityDuration => 0;
    public bool CanBeUsed => canBeUsed;
    public IAbility AbilityLock { 
        get => abilityLock;
        set{
            if((Object)value == (Object)this || value == null){
                abilityLock = value;
            }
        }
    }
    private IAbility abilityLock;
    #endregion

    private void Start() {
        player = Utility.ComponentFinder.FindComponentInParents<Player>(this.transform);
        AbilityLock = this;
    }

    public void UseAbility(bool inputReceived){
        if(inputReceived){
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        } 
    }

    public void ResetCooldown(){
        return;
    }
}
