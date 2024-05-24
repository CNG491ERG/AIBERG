using System.Collections;
using System.Collections.Generic;
using AIBERG.Core;
using TMPro;
using UnityEngine;

namespace AIBERG
{
    public class ScoreManager : MonoBehaviour
    {
        public GameEnvironment environment;
        public TextMeshProUGUI scoreText;
        public float scoreUpdateTimer = 0;
        private void Update() {
            if(scoreUpdateTimer >= 0.25f){
                scoreText.text = environment.scoreCounter.Score.ToString().PadLeft(10, '0');
                scoreUpdateTimer = 0;
            }
            scoreUpdateTimer += Time.deltaTime;
        }
    }
}
