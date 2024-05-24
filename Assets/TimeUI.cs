using System.Collections;
using System.Collections.Generic;
using AIBERG.Core;
using TMPro;
using UnityEngine;

namespace AIBERG
{
    public class TimeUI : MonoBehaviour
    {
        public GameEnvironment environment;
        public TextMeshProUGUI timerText;
        // Start is called before the first frame update

        private void Start() {
            timerText.text = "00:00";
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (environment.IsCountingSteps)
            {
                long totalSeconds = environment.StepCounter / 50;
                long minutes = totalSeconds / 60;
                long seconds = totalSeconds % 60;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
