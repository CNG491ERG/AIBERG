using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBERG
{
    public class RetryMode : MonoBehaviour
    {
        public void ReloadCurrentMode(){
            LevelLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
