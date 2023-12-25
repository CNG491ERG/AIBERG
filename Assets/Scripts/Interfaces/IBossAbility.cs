using UnityEngine;
public interface IBossAbility : IAbility{
    //Boss abilities can always have a reference to the player
    public Player PlayerObject{
        get{
            return AbilityOwner.transform.parent.Find("Player").GetComponent<Player>(); //Must change, not good solution
        }
    }
}
