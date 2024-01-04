using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timer = 0;
    
    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer/60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timerText.SetText(minutes.ToString("00") + ":" + seconds.ToString("00"));
    }
}
