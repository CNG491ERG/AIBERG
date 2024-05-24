using UnityEngine;

namespace AIBERG
{
    public class GoBackToMainMenu : MonoBehaviour
    {
        public void LoadMainMenu(){
            LevelLoader.Instance.LoadScene(0);
        }
    }
}
