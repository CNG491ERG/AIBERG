using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Jump : BaseAbility{
    [SerializeField] private Faction faction;
    [SerializeField] private float raycastDistance = 0.55f;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isGliding;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isOnPeakHeight;
    [SerializeField] private bool isReceivingInput;
    [SerializeField] private bool firstJumpComplete = false;
    [SerializeField] private float jumpSpeed = 20;
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
        isReceivingInput = Input.GetKey(KeyCode.Space);
        isGliding = !isOnGround & isReceivingInput;
        isFalling = !isOnGround & !isReceivingInput;
        UseAbility();
    }

    
    public override void UseAbility(){
        if(isOnGround){
            firstJumpComplete = false;
        }
        //Jump
        if(isReceivingInput && !firstJumpComplete){
             faction.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpSpeed);
        }
        else if((!isReceivingInput && !isOnGround) || isOnPeakHeight){ //Jump complete
            firstJumpComplete = true;
        }
        if(isOnPeakHeight){
            firstJumpComplete = true;
        }
        
        if(firstJumpComplete && isReceivingInput){ //Glide
            faction.player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -jumpSpeed*glideGravityMultiplier);
        }
        else if(!isReceivingInput){ //Falling or on ground
            faction.player.GetComponent<Rigidbody2D>().gravityScale = fallGravityMultiplier;
        }
        //Jumped and is still pressing -> Rise
        //Jumped, reached peak height, did not let go of space key -> Glide
        //Jumped, did not reach peak heigh, let go of space key -> Fall
        //Pressed space key while falling -> Glide
        
    }
}
