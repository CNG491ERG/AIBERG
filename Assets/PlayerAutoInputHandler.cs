using UnityEngine;

    public class PlayerAutoInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;//faction of the player
    [SerializeField] private Boss boss;//the boss
    [SerializeField] private Player player;//the player
    [SerializeField] private Transform point;//player spawn point as a reference

    //at start, gets the needed objects
    private void Start() {
        faction = transform.parent.GetComponentInChildren<Faction>();
        boss = transform.parent.parent.Find("Boss").GetComponent<Boss>();
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
        bool basicAbilityInput = true; //Always use basic input as it has no cooldown
        //checking cooldown on abilities
        bool ActiveAbility2Input = faction.ActiveAbility2.CanBeUsed;
        bool ActiveAbility1Input = faction.ActiveAbility1.CanBeUsed;
        bool JumpAbilityInput = false;

        //ActiveAbility2 directly aims at the boss, so no need to put logic
        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.ActiveAbility2.UseAbility(ActiveAbility2Input);

        RaycastHit2D raycastBelow = Physics2D.Raycast(point.transform.position, -point.transform.up, Mathf.Infinity, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D raycastAbove = Physics2D.Raycast(point.transform.position, point.transform.up, Mathf.Infinity, LayerMask.GetMask("ForegroundEnvironment"));
        //distance between the up and down limits is 8.02f
        //divide it by 3 parts and create Box Casts accordingly

        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(origin: new Vector2(5.7f + raycastAbove.point.x, raycastAbove.point.y - 1.36f), 
            size: new Vector2(10f, 2.7f), angle: 0f, direction: Vector2.right, distance: 10);
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(origin: new Vector2(5.7f + raycastAbove.point.x, raycastAbove.point.y - 4.068f),
            size: new Vector2(10f, 2.7f), angle: 0f, direction: Vector2.right, distance: 10);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(origin: new Vector2(5.7f + raycastAbove.point.x, raycastAbove.point.y - 6.72f),
            size: new Vector2(10f, 2.59f), angle: 0f, direction: Vector2.right, distance: 10);
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
        y = Search(-2.70f,0.0f,1.3f,2.0f, bossY);
        x = Search(-1.28f, -0.76f, 1.34f, 1.87f, playerY);

        //Debug.Log("player: "+x+" Boss: "+y);
        Debug.Log(p1+" is p1, "+p2+"is p2 and p3: "+p3);

        //main logic is applied here
        //according to the boss&player field and if there are any incoming attacks
        switch (x, y)
        {
            //field 0
            case (0, 0):
                if (p0 && ActiveAbility1Input) {//if p0 has an active attack and ActiveAbility is ready
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    JumpAbilityInput = true;
                }
                else if (p2! && p1) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 1):
                if (p1 && !p2) {
                    JumpAbilityInput = true;
                }
                break;
            case (0, 2):
                if(ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (0, 3):
                if(ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (0, 6):
                if (p6) {
                    JumpAbilityInput = true;
                }
                else if (!p2 && p3 && ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;

            //field 1
            case (1, 0):
                if ((p0 ||(p1 && !p2)) && ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 1):   
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input); 
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
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
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
                if ((!p2 && ActiveAbility1Input)) {
                    JumpAbilityInput = true;
                }
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
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
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                break;
        }
        faction.JumpAbility.UseAbility(JumpAbilityInput);
    }
}
