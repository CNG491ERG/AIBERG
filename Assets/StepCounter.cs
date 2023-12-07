using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepCounter : MonoBehaviour{
    private Int64 steps = 0;
    [SerializeField] private TextMeshProUGUI stepCounterText;
    private void FixedUpdate() {
        stepCounterText.SetText("Step #" + steps.ToString("0000000"));
        steps++;
    }
}
