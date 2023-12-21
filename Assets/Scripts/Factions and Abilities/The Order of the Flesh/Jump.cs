using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jump : MonoBehaviour, IPlayerAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private float raycastDistance = 0.55f;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isGliding;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isOnPeakHeight;
    [SerializeField] private bool inputReceived;
    [SerializeField] private bool firstJumpComplete = false;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private float glideGravityMultiplier = 0.3f;
    [SerializeField] private float jumpTimer = 2;
    // Start is called before the first frame update
    private void Start() {
        this.faction = GetComponentInParent<Faction>();
    }

    private Vector2 previousFrameVelocity = Vector2.zero;

    public Faction PlayerFaction => faction;

    public string AbilityName => "TheOrderOfTheFleshJump";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 0;

    public float AbilityDuration => 0;

    public bool CanBeUsed => true;

    public void UseAbility(bool inputReceived){
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        Debug.DrawLine(transform.position, transform.position-(transform.up*raycastDistance), Color.red); //Hit below visualization
        Debug.DrawLine(transform.position, transform.position+(transform.up*raycastDistance), Color.red); //Hit above visualization
        isOnGround = hitBelow.collider != null;
        isOnPeakHeight = hitAbove.collider != null;
        isGliding = !isOnGround & inputReceived;
        isFalling = !isOnGround & !inputReceived;

        if(isOnGround){
            firstJumpComplete = false;
            jumpTimer = 2;
        }
        if(inputReceived && !firstJumpComplete){ //Jump
            jumpTimer = jumpTimer >= 0 ? jumpTimer*0.95f : 0;
            faction.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce*jumpTimer));
        }
        else if((!inputReceived && !isOnGround) || isOnPeakHeight){ 
            firstJumpComplete = true;
        }
        if(isOnPeakHeight){
            firstJumpComplete = true;
        }
        
        if(firstJumpComplete && inputReceived && faction.player.GetComponent<Rigidbody2D>().velocity.y < 0){ //Glide
            faction.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, glideGravityMultiplier * jumpForce));
        }
        previousFrameVelocity = faction.player.GetComponent<Rigidbody2D>().velocity;  
    }
}
