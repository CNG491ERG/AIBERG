using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepCounter : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI stepCounterText;
    private void Update() {
        stepCounterText.SetText("Step #" + GameManager.Instance.StepCount.ToString("0000000"));
    }
}
