using UnityEngine;


namespace AIBERG.ScriptableObjects{
[CreateAssetMenu(fileName = "FactionData", menuName = "ScriptableObjects/FactionScriptableObject", order = 1)]
public class FactionSO : ScriptableObject{
    public string FactionName;

    public GameObject BasicAbility;
    public GameObject ActiveAbility1;
    public GameObject ActiveAbility2;
    public GameObject PassiveAbility;
    public GameObject Jump;

    public string BasicAbilityName;
    public string ActiveAbility1Name;
    public string ActiveAbility2Name;
    public string PassiveAbilityName;

    public float MaxHealth;
    public float Defense;

}
}