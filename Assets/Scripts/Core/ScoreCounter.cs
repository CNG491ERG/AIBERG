using UnityEngine;

namespace AIBERG.Core{
    public class ScoreCounter
    {
        [SerializeField] private long score;
        public long Score{get => score; private set => score = value;}
        public ScoreCounter(){
            ResetScore();
        }

        public void ResetScore(){
            score = 0;
        }

        public void AddScore(long scoreToAdd){
            Debug.Log(scoreToAdd);
            score += scoreToAdd;
        }
    }
}