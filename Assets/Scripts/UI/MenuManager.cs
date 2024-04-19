using UnityEngine;
using UnityEngine.SceneManagement;

namespace AIBERG.UI
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject mainMenuObject;
        public GameObject loginMenuObject;
        public GameObject registerMenuObject;
        public GameObject gameMenuObject;
        public GameObject currentMenu;
        public GameObject previousMenu;
        private void Start() {
            currentMenu = mainMenuObject;
            currentMenu.SetActive(true);
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void SwitchToLoginMenu(){
            previousMenu = currentMenu;
            currentMenu.SetActive(false);
            currentMenu = loginMenuObject;
            currentMenu.SetActive(true);
        }
        public void SwitchToRegisterMenu(){
            previousMenu = currentMenu;
            currentMenu.SetActive(false);
            currentMenu = registerMenuObject;
            currentMenu.SetActive(true);
        }
        public void QuitGame()
        {
            Application.Quit();
        }

    }
}

