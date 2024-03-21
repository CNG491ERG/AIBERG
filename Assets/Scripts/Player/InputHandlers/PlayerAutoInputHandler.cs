using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAutoInputHandler : InputHandler{
    public Transform boss;
    public Transform player;
    [SerializeField] private FactionSO faction;
    [SerializeField] private GameObject activeAbility1Object;
    public Transform playerSpawnPoint;//player spawn point as a reference
    IAbility a;
    public IAbility activeAbility1;

    //at start, gets the needed objects
    private void Start() {
        boss = Utility.ComponentFinder.FindComponentInParents<Environment>(this.transform).Boss.transform;
        player = Utility.ComponentFinder.FindComponentInParents<Environment>(this.transform).Player.transform;
        this.BasicAbilityInput = true;
        this.ActiveAbility2Input = true;

        //try
        //{
           if (faction.ActiveAbility1 != null)
            {
            activeAbility1Object = Instantiate(faction.ActiveAbility1, this.transform);
            activeAbility1 = activeAbility1Object.GetComponent<IAbility>();
             } 
        //}catch
        //{ print("Obj reference not set"); }
        

        /*Debug.DrawLine(new Vector2(0, 0), new Vector3(3, 3), Color.red, 2, false);
        Debug.DrawLine(new Vector2(-10, -5), new Vector3(3, 3), Color.red, 2, false);
        Debug.DrawLine(new Vector2(-100, 0), new Vector3(0, 100), Color.red, 2, false);
        Debug.DrawLine(new Vector3(200f, 200f, 0f), Vector3.zero, Color.green, 10000000000, false);
        Debug.DrawLine(new Vector3(200, 200, 200), Vector3.zero, Color.green, 10000000000000, false);*/


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

        //                       -3.6             -1.2             1.2              3.6  
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
        //bool ActiveAbility1Input = faction.ActiveAbility1.CanBeUsed;
        bool JumpAbilityInput = false;


        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(origin: new Vector2(-9f, 2.1f),
            size: new Vector2(6f, 3.8f), angle: 0f, direction: Vector2.right, distance: 10);
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(origin: new Vector2(-9f, -1.9f),
            size: new Vector2(6f, 3.8f), angle: 0f, direction: Vector2.right, distance: 10);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(origin: new Vector2(-9f, -5.9f),
            size: new Vector2(6f,3.8f), angle: 0f, direction: Vector2.right, distance: 10);


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
        y = Search(-3.6f,-1.2f,1.2f,3.6f, bossY);
        x = Search(-3.6f, -1.2f, 1.2f, 3.6f, playerY);

        
        //main logic is applied here
        //according to the boss&player field and if there are any incoming attacks
        switch (x, y)
        {
            //field 0
            case (0, 0):
                if (p0 && a.CanBeUsed) {//if p0 has an active attack and ActiveAbility is ready
                    this.ActiveAbility1Input = true;
                    JumpAbilityInput = true;
                }
                else if (p2! || p1) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 1):
                if ((p1 && !p2) || (!ActiveAbility1Input)) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 2):
                if(ActiveAbility1Input || p0)
                    JumpAbilityInput = true;
                break;
            case (0, 3):
                if(ActiveAbility1Input ||p0 ||p1)
                    JumpAbilityInput = true;
                break;
            case (0, 6):
                if (p6 || p0) {
                    JumpAbilityInput = true;
                }
                else if (!p2 && p3 && ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;

            //field 1
            case (1, 0):
                if ((p0 ||(p1)) || ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 1):   
                if(ActiveAbility1Input) {
                    this.ActiveAbility1Input = true; 
                }
                 else
                    JumpAbilityInput = true;
                break;
            case (1, 2):
                if ((!p1 && p2) || (p0)) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 3):
                if(ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (1, 6):
                JumpAbilityInput = true;
                break;

            //field 2
            case (2, 0):
                if (!p2 && !ActiveAbility1Input) {
                    JumpAbilityInput = true;        
                }
                else if (p0 && !ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;
            case (2, 1):
                if (!ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (2, 2):
                if (p1)
                    JumpAbilityInput = true;
                this.ActiveAbility1Input = true;
                break;
            case (2, 3):
                if ((p2 || ActiveAbility1Input))
                    JumpAbilityInput = true;
                break;
            case (2, 6):
                if ((p2 && ActiveAbility1Input) || (!p2))
                    JumpAbilityInput = true;
                break;

            //field 3
            case (3, 2):
                if (p1 && !p2 && p3 && !ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (3, 3):
                if ((ActiveAbility1Input)) {
                    JumpAbilityInput = true;
                }
                this.ActiveAbility1Input = true;
                break;
            case (3, 6):
                if ((p2 && !p3) || ActiveAbility1Input)
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
                if (p1 || (!p3 && !ActiveAbility1Input))
                    JumpAbilityInput = true;
                break;
            case (6, 3):
                if (ActiveAbility1Input)
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
