using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBERG
{
    public class RetryMode : MonoBehaviour
    {
        public void ReloadCurrentMode(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
