using Unity.Burst.CompilerServices;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerAutoInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private Boss boss;
    [SerializeField] private Player player;
    [SerializeField] private Transform point;

    private void Start() {
        faction = transform.parent.GetComponentInChildren<Faction>();
        boss = transform.parent.parent.Find("Boss").GetComponent<Boss>(); //Temporary solution
    }
    private void OnDrawGizmos()  {

        RaycastHit2D raycastBelow = Physics2D.Raycast(point.transform.position, -point.transform.up, Mathf.Infinity, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D raycastAbove = Physics2D.Raycast(point.transform.position, point.transform.up, Mathf.Infinity, LayerMask.GetMask("ForegroundEnvironment"));

        Vector2 middlePoint = (raycastAbove.point + raycastBelow.point) / 2;
        Gizmos.DrawLine(middlePoint, new Vector2(middlePoint.x * 2, middlePoint.y));
    }

    private bool Attackdetector(RaycastHit2D hit) {
        bool val = false;

        if (hit.collider != null) {
            if (hit.collider.GetComponent<DamagingProjectile>() != null) {
                if (hit.collider.GetComponent<Rigidbody2D>().velocity.x < 0) {
                    val = true;
                }
            }

        }
        return val;
    }

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
            if (coordinate < position1)
                return (1);
            else
                return (0);
        }

    }

    private void FixedUpdate() {
        bool basicAbilityInput = true; //Always use basic input as it has no cooldown
        bool ActiveAbility2Input = faction.ActiveAbility2.CanBeUsed;
        bool ActiveAbility1Input = faction.ActiveAbility1.CanBeUsed;
        bool JumpAbilityInput = false;

        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.ActiveAbility2.UseAbility(ActiveAbility2Input);
       
        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, 0.48f + point.localPosition.y), size: new Vector2(10f, 2), 0f, direction: transform.right);
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, -1.528f + point.localPosition.y), size: new Vector2(10f, 2), 0f, direction: transform.right);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, -3.534f + point.localPosition.y), size: new Vector2(10f, 1.965f), 0f, direction: transform.right);
        
        bool p1 = Attackdetector(LeftAttackDetector1);
        bool p2 = Attackdetector(LeftAttackDetector2);
        bool p3 = Attackdetector(LeftAttackDetector3);
        bool p0 = p1 && p2; 
        bool p6 = p2 && p3;
        int x, y;
        float bossY = boss.transform.localPosition.y;
        float playerY = player.transform.localPosition.y;

        y = Search(-2.74f,0.0f,1.3f,2.0f, bossY);
        x = Search(-1.28f, -0.76f, 1.34f, 1.87f, playerY);

        Debug.Log(x+" and "+y);
        Debug.Log(p1+" is p1, "+p2+"is p2 and p3: "+p3);

        switch (x, y)
        {
            case (0, 0):
                if (p0 && ActiveAbility1Input) {
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
            case (1, 0):
                if ((p0 ||(p1 && !p2)) && ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
                break;
            case (1, 1):   
                if (p1 && !ActiveAbility1Input) {
                    JumpAbilityInput = true;
                }
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
                break;
            case (2, 3):
                if ((p2 && ActiveAbility1Input))
                    JumpAbilityInput = true;
                break;
            case (2, 6):
                if ((p2 && ActiveAbility1Input) || (!p2))
                    JumpAbilityInput = true;
                break;

            case (3, 0):
                break;
            case (3, 1):
                break;
            case (3, 2):
                if (p1 && !p2 && p3 && !ActiveAbility1Input)
                    JumpAbilityInput = true;
                break;
            case (3, 3):
                if ((!p2 && ActiveAbility1Input)) {
                    JumpAbilityInput = true;
                }
                break;
            case (3, 6):
                if (p2 && !p3)
                    JumpAbilityInput = true;
                break;

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
                if (p2)
                    JumpAbilityInput = true;
                break;
        }
        faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
        faction.JumpAbility.UseAbility(JumpAbilityInput);
    }
    
}
