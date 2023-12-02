using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class SimpleDecisionMaker : MonoBehaviour{
    [SerializeField] private bool moveUpInput;
    [SerializeField] private bool moveDownInput;

    [SerializeField] private BaseAbility moveUpAbility;
    [SerializeField] private BaseAbility moveDownAbility;

    [SerializeField] private float raycastDistance = 1f;


    [SerializeField] private Rigidbody2D bossRb;
    private void Start() {
        bossRb = GetComponent<Rigidbody2D>();
        bossRb.velocity = new Vector2(0, 0.1f);
    }

    // Update is called once per frame
    void Update(){
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        Debug.DrawLine(transform.position, transform.position-(transform.up*raycastDistance), Color.green); //Hit below visualization
        Debug.DrawLine(transform.position, transform.position+(transform.up*raycastDistance), Color.green); //Hit above visualization
        
        moveUpInput = hitAbove.collider == null && bossRb.velocity.y >= 0f;
        moveDownInput = hitBelow.collider == null && bossRb.velocity.y <= 0f;

        moveUpAbility.UseAbility(moveUpInput);
        moveDownAbility.UseAbility(moveDownInput);
    }
}
