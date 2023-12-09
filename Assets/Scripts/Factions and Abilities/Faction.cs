using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
** Attach "Faction" component to an empty prefab
** create empty child objects below faction object
** attach abilities one by one to each child object
*/
public class Faction : MonoBehaviour{
    public Player player;
    [SerializeField] private MonoBehaviour basicAttack;
    [SerializeField] private MonoBehaviour activeAbility1;
    [SerializeField] private MonoBehaviour activeAbility2;
    [SerializeField] private MonoBehaviour passiveAbility;
    [SerializeField] private MonoBehaviour jumpAbility;
    public IPlayerAbility BasicAttack => basicAttack.GetComponent<IPlayerAbility>();
    public IPlayerAbility ActiveAbility1 => activeAbility1.GetComponent<IPlayerAbility>();
    public IPlayerAbility ActiveAbility2 => activeAbility2.GetComponent<IPlayerAbility>();
    public IPlayerAbility PassiveAbility => passiveAbility.GetComponent<IPlayerAbility>();
    public IPlayerAbility JumpAbility => jumpAbility.GetComponent<IPlayerAbility>();
    public float health;
    public float defense; //defense between 0-1
    public string factionName;
    private void Awake() {
        player = GetComponentInParent<Player>();
    }
}
