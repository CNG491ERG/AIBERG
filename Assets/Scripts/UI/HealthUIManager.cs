using AIBERG.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace AIBERG.UI{
public class HealthUIManager : MonoBehaviour{
    public GameObject healthOwner;
    public Image healthBar;
    private IDamageable healthOwnerComponent;
    private void Start() {
        healthOwnerComponent = healthOwner.GetComponent<IDamageable>();
    }
    private void Update() {
        healthBar.fillAmount = healthOwnerComponent.Health/healthOwnerComponent.MaxHealth;
    }
}

}
