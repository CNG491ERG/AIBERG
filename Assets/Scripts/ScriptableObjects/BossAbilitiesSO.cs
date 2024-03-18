using UnityEngine;

[CreateAssetMenu(fileName = "BossAbilities", menuName = "ScriptableObjects/BossAbilitiesScriptableObject", order = 1)]
public class BossAbilitiesSO : ScriptableObject{
    public GameObject MoveUpAbility;
    public GameObject MoveDownAbility;
    public GameObject BasicAbility;
    public GameObject AttackDroneAbility;
    public GameObject Jump;
    public float MaxHealth;
    public float Defense;
    public float Speed;
}
