using UnityEngine;

[CreateAssetMenu(fileName = "BossAbilities", menuName = "ScriptableObjects/BossAbilitiesScriptableObject", order = 1)]
public class BossAbilitiesSO : ScriptableObject{
    public GameObject MoveUpAbility;
    public GameObject MoveDownAbility;
    public GameObject BasicAttackAbility;
    public GameObject AttackDroneAbility;
    public float MaxHealth;
    public float Defense;
    public float Speed;
}
