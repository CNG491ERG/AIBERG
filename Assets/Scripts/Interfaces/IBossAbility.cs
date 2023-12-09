using UnityEngine;
public interface IBossAbility : IAbility{
    //Boss abilities can always have a reference to the player
    public Player PlayerObject{
        get{
            return GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }
}
