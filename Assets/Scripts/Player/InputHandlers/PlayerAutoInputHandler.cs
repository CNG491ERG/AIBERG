using System.Linq;
using UnityEngine;

public class PlayerAutoInputHandler : InputHandler{

    [SerializeField] private Environment environment;
    [SerializeField] private Boss boss;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform topLeftCorner;
    [SerializeField] private Transform bottomRightCorner;
    IAbility a;

    //at start, gets the needed objects
    private void Start() {
        environment = Utility.ComponentFinder.FindComponentInParents<Environment>(this.transform);
        boss = environment.Boss;
        player = environment.Player;
        topLeftCorner = environment.ForegroundObjects.First(foregroundObject => foregroundObject.name == "TopLeftCorner").transform;
        bottomRightCorner = environment.ForegroundObjects.First(foregroundObject => foregroundObject.name == "BottomRightCorner").transform;
        playerSpawnPosition = environment.PlayerSpawnPosition;

        this.BasicAbilityInput = true;
        this.ActiveAbility2Input = true;

    }

    //Detects any attacks coming to the collider
    private bool Attackdetector(RaycastHit2D hit) {
        bool val = false;
    
        if (hit.collider != null) {//Checks if there is a collision
            if (hit.collider.GetComponent<DamagingProjectile>() != null) {//checks if it is a Damaging Projectile type
                if (hit.collider.GetComponent<Rigidbody2D>().velocity.x < 0) {//Checks if it is coming from the boss
                    val = true;
                }
            }
        }
        return val;
    }

        //Searchs the rightful position for objects
        //--------------------------------------------------------------------
        //|                                                                  |
        //|                               3                                  |
        //|                                                                  |
        //--------------------------------------------------------------------  position 4
        //|                                                                  |
        //|                               6                                  |
        //|                                                                  |
        //--------------------------------------------------------------------  position 3
        //|                                                                  |
        //|                               2                                  |
        //|                                                                  |
        //--------------------------------------------------------------------  position 2
        //|                                                                  |
        //|                               0                                  |
        //|                                                                  |
        //--------------------------------------------------------------------  position 1
        //|                                                                  |
        //|                               1                                  |
        //|                                                                  |
        //--------------------------------------------------------------------

        private int Search(float position1, float position2, float position3, float position4, float coordinate) {
        if (coordinate > position2) {
            if (coordinate > position3) {
                if (coordinate > position4)
                    return (3);
                else
                    return (6);
            }
            else
                return (2);
        }
        else {
            if (coordinate > position1)
                return (0);
            else
                return (1);
        }

    }

    private void FixedUpdate() {
        this.BasicAbilityInput = true;
        this.ActiveAbility2Input = true;
        bool ActiveAbility2Input = this.ActiveAbility1Input;
        bool JumpAbilityInput = false;


        float bottomRightCornerX = bottomRightCorner.localPosition.x;
        float bottomRightCornerY = bottomRightCorner.localPosition.y;
        float topLeftCornerX = topLeftCorner.localPosition.x;
        float topLeftCornerY = topLeftCorner.localPosition.y;

        float verticalLen = topLeftCornerY - bottomRightCornerY;
        float horizontalLen = bottomRightCornerX - topLeftCornerX;

        float divided3 = verticalLen / 3f;
        float divided5 = verticalLen / 5f;



        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(origin: new Vector2(horizontalLen/4, bottomRightCornerY),
            size: new Vector2((3*horizontalLen)/4, bottomRightCornerY + divided3), angle: 0f, direction: Vector2.right, distance: 10); 
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(origin: new Vector2(horizontalLen / 4, -1.9f),
            size: new Vector2((3 * horizontalLen) / 4, bottomRightCornerY + (2*divided3)), angle: 0f, direction: Vector2.right, distance: 10);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(origin: new Vector2(horizontalLen / 4, -5.9f),
            size: new Vector2((3 * horizontalLen) / 4, bottomRightCornerY + (3 * divided3)), angle: 0f, direction: Vector2.right, distance: 10);


        //detects if collision happens in any of the fields, logic is shaped using these sensors
        bool p1 = Attackdetector(LeftAttackDetector1);
        bool p2 = Attackdetector(LeftAttackDetector2);
        bool p3 = Attackdetector(LeftAttackDetector3);
        bool p0 = p1 && p2; 
        bool p6 = p2 && p3;
        //checks if there are no boss attacks
        bool empty =  p0 && p6;
        int x, y;
        //gets the y positions of the boss and the player
        float bossY = boss.transform.localPosition.y;
        float playerY = player.transform.localPosition.y;


        //searchs their fields(1,0,2,6,3)
        y = Search(bottomRightCornerY+ divided5, bottomRightCornerY + (2*divided5), bottomRightCornerY + (3*divided5), bottomRightCornerY + (4*divided5), bossY);
        x = Search(bottomRightCornerY + divided5, bottomRightCornerY + (2 * divided5), bottomRightCornerY + (3 * divided5), bottomRightCornerY + (4 * divided5), playerY);

      
        //main logic is applied here
        //according to the boss&player field and if there are any incoming attacks
        switch (x, y)
        {
            //field 0
            case (0, 0):
                if (p0) {//if p0 has an active attack and ActiveAbility is ready
                    this.ActiveAbility1Input = true;
                    JumpAbilityInput = true;
                }
                else if (p2! || p1) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 1):
                if ((p1 && !p2)) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 2):
                if( p0)
                    JumpAbilityInput = true;
                break;
            case (0, 3):
                if(p0 ||p1)
                    JumpAbilityInput = true;
                break;
            case (0, 6):
                if (p6 || p0) {
                    JumpAbilityInput = true;
                }
                else if (!p2 && p3) {
                    JumpAbilityInput = true;
                }
                break;

            //field 1
            case (1, 0):
                if ((p0 ||(p1))) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 1):   
                 this.ActiveAbility1Input = true; 
                 JumpAbilityInput = true;
                break;
            case (1, 2):
                if ((!p1 && p2) || (p0)) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 3):
                JumpAbilityInput = true;
                break;
            case (1, 6):
                JumpAbilityInput = true;
                break;

            //field 2
            case (2, 0):
                if (!p2) {
                    JumpAbilityInput = true;        
                }
                else if (p0) {
                    JumpAbilityInput = true;
                }
                break;
            case (2, 1):
                JumpAbilityInput = true;
                break;
            case (2, 2):
                if (p1)
                    JumpAbilityInput = true;
                this.ActiveAbility1Input = true;
                break;
            case (2, 3):
                if (p2)
                    JumpAbilityInput = true;
                break;
            case (2, 6):
                if (p2|| !p2)
                    JumpAbilityInput = true;
                break;

            //field 3
            case (3, 2):
                if (p1 && !p2 && p3)
                    JumpAbilityInput = true;
                break;
            case (3, 3):

                    JumpAbilityInput = true;

                this.ActiveAbility1Input = true;
                break;
            case (3, 6):
                if ((p2 && !p3))
                    JumpAbilityInput = true;
                break;

            //field 6
            case (6, 0):
                if (p2 && !p1)
                    JumpAbilityInput = true;
                break;
            case (6, 1):
                break;
            case (6, 2):
                if (p1 || (!p3))
                    JumpAbilityInput = true;
                break;
            case (6, 3):
                    JumpAbilityInput = true;
                break;
            case (6, 6):
                if (p2 || JumpAbilityInput)
                    JumpAbilityInput = true;
                this.ActiveAbility1Input = true;
                break;
        }
        this.JumpInput =JumpAbilityInput;
    }
}
