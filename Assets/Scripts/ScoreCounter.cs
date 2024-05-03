using UnityEngine;

namespace AIBERG.Core{
    public class ScoreCounter : MonoBehaviour
    {

        [SerializeField] private long score;
        void Start()
        {
            ResetScore();
        }

        public void ResetScore(){
            score = 0;
        }

        public void AddScore(long scoreToAdd){
            score += scoreToAdd;
        }
    }
}
