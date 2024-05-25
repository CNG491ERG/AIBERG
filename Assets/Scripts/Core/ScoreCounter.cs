using UnityEngine;

namespace AIBERG.Core
{
    public class ScoreCounter
    {
        [SerializeField] private long score;
        public long Score { get => score; private set => score = value; }
        public bool canAddScore = true;
        public ScoreCounter()
        {
            ResetScore();
        }

        public void ResetScore()
        {
            score = 0;
        }

        public void AddScore(long scoreToAdd)
        {
            if (canAddScore)
            {
                score += scoreToAdd;
            }

        }
    }
}