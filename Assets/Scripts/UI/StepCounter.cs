using TMPro;
using UnityEngine;

namespace AIBERG.UI{

public class StepCounter : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI stepCounterText;
    [SerializeField] private AIBERG.Environment.Environment environment;
    private void Update() {
        stepCounterText.SetText("Step #" + environment.StepCounter.ToString("0000000"));
    }
}

}
