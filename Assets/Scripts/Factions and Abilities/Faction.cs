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
    public BaseAbility basicAttack;
    public BaseAbility activeAbility1;
    public BaseAbility activeAbility2;
    public BaseAbility passiveAbility;
    public BaseAbility jumpAbility;
    public float health;
    public string factionName;
    private void Awake() {
        player = GetComponentInParent<Player>();
    }
    void Start(){
        BaseAbility[] abilities = GetComponentsInChildren<BaseAbility>();
        basicAttack = abilities.Where(ability => ability.abilityType == AbilityType.BASIC).First();
        jumpAbility = abilities.Where(ability => ability.abilityType == AbilityType.JUMP).First();
    }
}
