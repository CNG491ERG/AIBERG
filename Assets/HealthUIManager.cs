using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUIManager : MonoBehaviour
{
    public GameObject healthOwner;
    public Image healthBar;
    private IDamageable healthOwnerComponent;
    private void Start() {
        healthOwnerComponent = healthOwner.GetComponent<IDamageable>();
    }
    private void Update() {
        healthBar.fillAmount = healthOwnerComponent.Health/100;
    }
}
