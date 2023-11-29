using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jump : BaseAbility{
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
    [SerializeField] private float fallGravityMultiplier = 4.0f;
    // Start is called before the first frame update
    private void Start() {
        this.faction = GetComponentInParent<Faction>();
        this.abilityName = "Jump";
        this.abilityOwner = faction.player.gameObject;
        this.abilityType = AbilityType.JUMP;
        this.cooldown = 0;
        this.damage = 0;
        this.duration = 0;
    }

    private void FixedUpdate() {
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        Debug.DrawLine(transform.position, transform.position-(transform.up*raycastDistance), Color.red); //Hit below visualization
        Debug.DrawLine(transform.position, transform.position+(transform.up*raycastDistance), Color.red); //Hit above visualization
        isOnGround = hitBelow.collider != null;
        isOnPeakHeight = hitAbove.collider != null;
        inputReceived = Input.GetKey(KeyCode.Space);
        isGliding = !isOnGround & inputReceived;
        isFalling = !isOnGround & !inputReceived;
        UseAbility(inputReceived);
    }

    [SerializeField] private float jumpTimer = 2;
    public override void UseAbility(bool inputReceived){
        if(isOnGround){
            firstJumpComplete = false;
            jumpTimer = 2;
        }
        if(inputReceived && !firstJumpComplete){
            jumpTimer = jumpTimer >= 0 ? jumpTimer*0.95f : 0;
            faction.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce*jumpTimer));
        }
        else if((!inputReceived && !isOnGround) || isOnPeakHeight){ 
            firstJumpComplete = true;
        }
        if(isOnPeakHeight){
            firstJumpComplete = true;
        }
        
        if(firstJumpComplete && this.inputReceived){ //Glide
            faction.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            faction.player.GetComponent<Rigidbody2D>().gravityScale = glideGravityMultiplier;
        }
        else if(!inputReceived){ //Falling or on ground
            faction.player.GetComponent<Rigidbody2D>().gravityScale = fallGravityMultiplier;
        }
    }
}
