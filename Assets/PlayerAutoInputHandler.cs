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

    private void FixedUpdate() {
        bool basicAbilityInput = true; //Always use basic input as it has no cooldown
        bool ActiveAbility2Input = faction.ActiveAbility2.CanBeUsed;
        bool ActiveAbility1Input = faction.ActiveAbility1.CanBeUsed;

        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.ActiveAbility2.UseAbility(ActiveAbility2Input);
       // faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
        //LayerMask mask = LayerMask.GetMask("Faction_TheOrderOfTheFlesh");
        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, 0.48f + point.localPosition.y), size: new Vector2(10f, 2), 0f, direction: transform.right);
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, -1.528f + point.localPosition.y), size: new Vector2(10f, 2), 0f, direction: transform.right);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, -3.534f + point.localPosition.y), size: new Vector2(10f, 1.965f), 0f, direction: transform.right);

        bool p1 = false, p2 = false, p3 = false;



        if (LeftAttackDetector1.collider != null) {
            if (LeftAttackDetector1.collider.GetComponent<DamagingProjectile>() != null)
            {
                if (LeftAttackDetector1.collider.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    p1 = true;
                }
            }

        }
        if (LeftAttackDetector2.collider != null) {
            if (LeftAttackDetector2.collider.GetComponent<DamagingProjectile>() != null)
            {
                if (LeftAttackDetector2.collider.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    p2 = true;
                }
                //if(faction.ActiveAbility1.AbilityDuration > 0)
                    
            }

        }
        if (LeftAttackDetector3.collider != null) {
            if (LeftAttackDetector3.collider.GetComponent<DamagingProjectile>() != null)
            {
                if (LeftAttackDetector3.collider.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    p3 = true;
                }
            }

        }


        Debug.DrawLine(new Vector2(5.7f + point.localPosition.x, 0.48f + point.localPosition.y), new Vector2(5.7f + point.localPosition.x, 0.48f + point.localPosition.y) + new Vector2(10f, 2), Color.black);






        int x, y;

        if (boss.transform.localPosition.y > 2f)
            y = 3;
        else if (boss.transform.localPosition.y > 1.3f)
            y = 6;
        else if (boss.transform.localPosition.y > 0.0f)
            y = 2;
        else if (boss.transform.localPosition.y> -1.3f)
            y = 0;
        else
            y = 1;


        if (player.transform.localPosition.y > 1.87f)
            x = 3;
        else if (player.transform.localPosition.y > 1.34f)
            x = 6;
        else if (player.transform.localPosition.y > -0.76f)
            x = 2;
        else if (player.transform.localPosition.y > -1.28f)
            x = 0;
        else
            x = 1;

       // Debug.Log(x+" and "+y);
        Debug.Log(p1+" is p1, "+p2+"is p2 and p3: "+p3);
        switch (x, y)
        {
            case (0, 0):
                if (p2 && !p1)
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                }
                else if (p2 && p1 && ActiveAbility1Input)
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    faction.JumpAbility.UseAbility(true);
                }
                else if (p2! && p1)
                {
                    faction.JumpAbility.UseAbility(true);
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                }
                break;
            case (0, 1):
                if (p2 && p1)
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                }
                else if (p1 && !p2)
                {
                    if (ActiveAbility1Input)
                        faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (0, 2):
                faction.JumpAbility.UseAbility(true);
                break;
            case (0, 3):
                if(ActiveAbility1Input)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (0, 6):
                if (p3 && p2)
                {
                    faction.JumpAbility.UseAbility(true);
                }
                else if (!p2 && p3 && ActiveAbility1Input)
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (1, 0):
                if (p1 && p2 && ActiveAbility1Input)
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    faction.JumpAbility.UseAbility(true);
                }
                else if (p1 && !p2 && ActiveAbility1Input)
                {
                    faction.JumpAbility.UseAbility(true);
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                }
                break;
            case (1, 1):
                
                if (p1 && !ActiveAbility1Input) {
                    faction.JumpAbility.UseAbility(true);
                }
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                break;
            case (1, 2):
                if ((!p1 && p2) || (p1 && p2))
                {
                    if (ActiveAbility1Input)
                        faction.JumpAbility.UseAbility(true);
                }
                else
                    faction.JumpAbility.UseAbility(true);
                break;
            case (1, 3):
                faction.JumpAbility.UseAbility(true);
                break;
            case (1, 6):
                faction.JumpAbility.UseAbility(true);
                break;

            case (2, 0):
                if (!p2) {
                    if (ActiveAbility1Input) {
                        faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    }
                    else
                        faction.JumpAbility.UseAbility(true);
                }
                else if (p1 && p2) {
                    if (!ActiveAbility1Input)
                        faction.JumpAbility.UseAbility(true);
                }
                break;
            case (2, 1):
                if (!ActiveAbility1Input)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 2):
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                if (p1)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 3):
                if ((p2 && ActiveAbility1Input))
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 6):
                if ((p2 && ActiveAbility1Input) || (!p2))
                    faction.JumpAbility.UseAbility(true);
                break;

            case (3, 0):
                break;
            case (3, 1):
                break;
            case (3, 2):
                if (!p1 && p2 && !p3 && ActiveAbility1Input)
                    faction.JumpAbility.UseAbility(false);
                else
                    faction.JumpAbility.UseAbility(true);
                break;
            case (3, 3):
                if ((!p2 && ActiveAbility1Input))
                {
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                    faction.JumpAbility.UseAbility(true);
                }
                else
                    faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                break;
            case (3, 6):
                if (p2 && !p3)
                    faction.JumpAbility.UseAbility(true);
                break;

            case (6, 0):
                if (p2 && !p1)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 1):
                break;
            case (6, 2):
                if (p1 || (!p3 && !ActiveAbility1Input))
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 3):
                if (ActiveAbility1Input)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 6):
                faction.ActiveAbility1.UseAbility(ActiveAbility1Input);
                if (p2)
                    faction.JumpAbility.UseAbility(true);
                break;
        }
       


        /*bool activeAbility1Input = faction.ActiveAbility1.CanBeUsed;
        bool activeAbility2Input = faction.ActiveAbility2.CanBeUsed;
        faction.BasicAttack.UseAbility(basicAbilityInput);
        faction.ActiveAbility1.UseAbility(activeAbility1Input);
        faction.ActiveAbility2.UseAbility(activeAbility2Input);*/





    }
}
