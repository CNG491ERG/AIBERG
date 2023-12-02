using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable{

    private float health;
    private float defense;
    private Faction playerFaction;

   
    // Start is called before the first frame update
    void Start(){
        playerFaction = GetComponentInChildren<Faction>();
        health = playerFaction.health;
        defense = playerFaction.defense;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public float Health{
        get{
            return health;
        }
        set{
            health = value;
        }
    }

    public float Defense {
        get{
            return defense;
        }
        set{
            defense = value;
        }
    }

    public void TakeDamage(float damageToTake){
        float totalDamage = damageToTake * (1 - Defense);
        Health = Health - totalDamage <= 0 ? 0 : Health - totalDamage;
    }

}
