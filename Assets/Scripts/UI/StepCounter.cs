using AIBERG.Core;
using TMPro;
using UnityEngine;

namespace AIBERG.UI{
public class StepCounter : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI stepCounterText;
    [SerializeField] private GameEnvironment environment;
    private void Update() {
        stepCounterText.SetText("Step #" + environment.StepCounter.ToString("0000000"));
    }
}

}
