using TMPro;
using UnityEngine;

public class StepCounter : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI stepCounterText;
    [SerializeField] private Environment environment;
    private void Update() {
        stepCounterText.SetText("Step #" + environment.StepCounter.ToString("0000000"));
    }
}
