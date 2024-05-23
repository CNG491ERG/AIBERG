using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBERG
{
    public class GoBackToMainMenu : MonoBehaviour
    {
        public void LoadMainMenu(){
            SceneManager.LoadScene(0);
        }
    }
}
