using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleDecisionMaker : MonoBehaviour{
    [SerializeField] private bool moveUpInput;
    [SerializeField] private bool moveDownInput;
    [SerializeField] private bool basicAttackInput;

    [SerializeField] private IAbility moveUpAbility;
    [SerializeField] private IAbility moveDownAbility;
    [SerializeField] private IAbility basicAttackAbility;

    [SerializeField] private float raycastDistance = 1f;


    [SerializeField] private Rigidbody2D bossRb;

    [SerializeField] private EventViewer eventViewer;
    private void Start() {
        /*
        eventViewer = GetComponent<EventViewer>();
        bossRb = GetComponent<Rigidbody2D>();
        bossRb.velocity = new Vector2(0, 0.1f); //Must have an initial velocity for input booleans to work correctly.
        moveUpAbility = GetComponents<IBossAbility>().Where(ability => ability.AbilityName == "MoveUp").First();
        moveDownAbility = GetComponents<IBossAbility>().Where(ability => ability.AbilityName == "MoveDown").First();
        basicAttackAbility = GetComponents<IBossAbility>().Where(ability => ability.AbilityName == "BasicAttack").First();*/
    }
    void FixedUpdate(){
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        Debug.DrawLine(transform.position, transform.position-(transform.up*raycastDistance), Color.green); //Hit below visualization
        Debug.DrawLine(transform.position, transform.position+(transform.up*raycastDistance), Color.green); //Hit above visualization
        
        moveUpInput = hitAbove.collider == null && bossRb.velocity.y >= 0f;
        moveDownInput = hitBelow.collider == null && bossRb.velocity.y <= 0f;
        basicAttackInput = (basicAttackAbility as BasicAttack).CanBeUsed;

        if(moveUpInput){
            eventViewer.eventsBeingPerformed.Add("MoveUp");
        }
        if(moveDownInput){
            eventViewer.eventsBeingPerformed.Add("MoveDown");
        }
        if(basicAttackInput){
            eventViewer.eventsBeingPerformed.Add("BasicAttack");
        }

        moveUpAbility.UseAbility(moveUpInput);
        moveDownAbility.UseAbility(moveDownInput);
        basicAttackAbility.UseAbility(basicAttackInput);
    }
}
