using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerAutoInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;
    [SerializeField] private Boss boss; 
    [SerializeField] private Transform point;

    private void Start() {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }


    private void FixedUpdate() {
        bool basicAbilityInput = true; //Always use basic input as it has no cooldown
        faction.BasicAttack.UseAbility(basicAbilityInput);
        RaycastHit2D LeftAttackDetector4 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, 2.485f + point.localPosition.y), size: new Vector2(10f, 2), 0f, transform.right);
        RaycastHit2D LeftAttackDetector3 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, 0.48f + point.localPosition.y), size: new Vector2(10f, 2), 0f, transform.right);
        RaycastHit2D LeftAttackDetector2 = Physics2D.BoxCast(new Vector2(5.7f + point.localPosition.x, -1.528f + point.localPosition.y), size: new Vector2(10f, 2), 0f, transform.right);
        RaycastHit2D LeftAttackDetector1 = Physics2D.BoxCast(new Vector2(5.7f + point.transform.localPosition.x, -3.534f + point.localPosition.y), size: new Vector2(10f, 1.965f), 0f, transform.right);
        bool jumpInput = false;
        int x = 5, y = 5;

        switch (x, y)
        {
            case (0, 0):
                if (LeftAttackDetector2 != null && LeftAttackDetector1 == null)
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                }
                else if (LeftAttackDetector2 != null && LeftAttackDetector1 != null && faction.ActiveAbility1.CanBeUsed)
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    faction.JumpAbility.UseAbility(true);
                }
                else if (LeftAttackDetector2 == null && LeftAttackDetector1 != null)
                {
                    faction.JumpAbility.UseAbility(true);
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                }
                break;
            case (0, 1):
                if (LeftAttackDetector2 != null && LeftAttackDetector1 != null)
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                }
                else if (LeftAttackDetector1 != null && LeftAttackDetector2 == null)
                {
                    if (faction.ActiveAbility1.CanBeUsed)
                        faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (0, 2):
                faction.JumpAbility.UseAbility(true);
                break;
            case (0, 3):
                faction.JumpAbility.UseAbility(true);
                break;
            case (0, 6):
                if (LeftAttackDetector3 != null && LeftAttackDetector2 != null)
                {
                    faction.JumpAbility.UseAbility(true);
                }
                else if (LeftAttackDetector2 == null && LeftAttackDetector3 != null && faction.ActiveAbility1.CanBeUsed)
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (1, 0):
                if (LeftAttackDetector1 != null && LeftAttackDetector2 != null && faction.ActiveAbility1.CanBeUsed)
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    faction.JumpAbility.UseAbility(true);
                }
                else if (LeftAttackDetector1 != null && LeftAttackDetector1 == null && faction.ActiveAbility1.CanBeUsed)
                {
                    faction.JumpAbility.UseAbility(true);
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                }
                break;
            case (1, 1):
                faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                if (LeftAttackDetector1 != null && LeftAttackDetector2 == null)
                {
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (1, 2):
                if ((LeftAttackDetector1 == null && LeftAttackDetector2 != null) || (LeftAttackDetector1 != null && LeftAttackDetector2 != null))
                {
                    if (faction.ActiveAbility1.CanBeUsed)
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
                if (LeftAttackDetector2 == null)
                {
                    if (faction.ActiveAbility1.CanBeUsed)
                    {
                        faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    }
                    else
                        faction.JumpAbility.UseAbility(true);
                }
                else if (LeftAttackDetector2 != null && LeftAttackDetector2 != null)
                {
                    if (!faction.ActiveAbility1.CanBeUsed)
                        faction.JumpAbility.UseAbility(true);
                }
                break;
            case (2, 1):
                if (!faction.ActiveAbility1.CanBeUsed)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 2):
                faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                if (LeftAttackDetector1 != null)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 3):
                if ((LeftAttackDetector2 != null && faction.ActiveAbility1.CanBeUsed) || (LeftAttackDetector2 == null))
                    faction.JumpAbility.UseAbility(true);
                break;
            case (2, 6):
                if ((LeftAttackDetector2 != null && faction.ActiveAbility1.CanBeUsed) || (LeftAttackDetector2 == null))
                    faction.JumpAbility.UseAbility(true);
                break;

            case (3, 0):
                break;
            case (3, 1):
                break;
            case (3, 2):
                if (LeftAttackDetector1 == null && LeftAttackDetector2 != null && LeftAttackDetector3 == null && faction.ActiveAbility1.CanBeUsed)
                    faction.JumpAbility.UseAbility(false);
                else
                    faction.JumpAbility.UseAbility(true);
                break;
            case (3, 3):
                if ((LeftAttackDetector2 == null && faction.ActiveAbility1.CanBeUsed) || (LeftAttackDetector2 != null))
                {
                    faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                    faction.JumpAbility.UseAbility(true);
                }
                break;
            case (3, 6):
                if (LeftAttackDetector2 != null && LeftAttackDetector3 == null)
                    faction.JumpAbility.UseAbility(true);
                break;

            case (6, 0):
                if (LeftAttackDetector2 != null && LeftAttackDetector1 == null)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 1):
                break;
            case (6, 2):
                if (LeftAttackDetector1 != null || (LeftAttackDetector3 == null && !faction.ActiveAbility1.CanBeUsed))
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 3):
                if (faction.ActiveAbility1.CanBeUsed)
                    faction.JumpAbility.UseAbility(true);
                break;
            case (6, 6):
                faction.ActiveAbility1.UseAbility(faction.ActiveAbility1.CanBeUsed);
                if (LeftAttackDetector2 != null)
                    faction.JumpAbility.UseAbility(true);
                break;
        }
    }
}
