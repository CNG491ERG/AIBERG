using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jump : MonoBehaviour, IPlayerAbility
{
    [SerializeField] private Faction faction;
    [SerializeField] private float raycastDistance = 0.55f;
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool inputReceived;
    [SerializeField] private float jumpForce = 20;
    // Start is called before the first frame update
    private void Start()
    {
        this.faction = GetComponentInParent<Faction>();
    }

    public Faction PlayerFaction => faction;

    public string AbilityName => "TheOrderOfTheFleshJump";

    public GameObject AbilityOwner => faction.player.gameObject;

    public float Cooldown => 0;

    public float AbilityDuration => 0;

    public bool CanBeUsed => true;

    public void UseAbility(bool inputReceived)
    {
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, transform.up, raycastDistance, LayerMask.GetMask("ForegroundEnvironment"));
        Debug.DrawLine(transform.position, transform.position - (transform.up * raycastDistance), Color.red); //Hit below visualization
        Debug.DrawLine(transform.position, transform.position + (transform.up * raycastDistance), Color.red); //Hit above visualization
        isFalling = !isOnGround & !inputReceived;

        if (inputReceived)
        {
            faction.player.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, jumpForce));
        }
        else if (isFalling)
        {
            faction.player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jumpForce));
        }
    }
    public void ResetCooldown()
    {
        return;
    }
}
