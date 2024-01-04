using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreCounterText;
   private void Update()
    {
        ScoreCounterText.SetText("Score: " + GameManager.Instance.Score);
    }
}
