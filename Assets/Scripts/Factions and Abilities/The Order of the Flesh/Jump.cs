using UnityEngine;

public class Jump : MonoBehaviour, IPlayerAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private float jumpForce;
    public Faction PlayerFaction => faction;
    public string AbilityName => "TheOrderOfTheFleshJump";
    public GameObject AbilityOwner => faction.player.gameObject;
    public float Cooldown => 0;
    public float AbilityDuration => 0;
    public bool CanBeUsed => true;

    //get faction component
    private void Start() {
        this.faction = GetComponentInParent<Faction>();
    }

    //when ability is called, check if input is given
    public void UseAbility(bool inputReceived){
        if(inputReceived){//add force to the player to make it jump
            faction.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        } 
    }

    public void ResetCooldown(){
        return;
    }
}
