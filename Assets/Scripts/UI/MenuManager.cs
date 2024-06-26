using UnityEngine;
using AIBERG.API;
using System;

namespace AIBERG.UI
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject mainMenuObject;
        public GameObject loginMenuObject;
        public GameObject registerMenuObject;
        public GameObject gameModeSelectionMenuObject;
        public GameObject currentMenu;
        public GameObject previousMenu;

        private LoginController loginController;

        private void Start() {
            loginController = loginMenuObject.GetComponent<LoginController>();
            loginController.OnSuccessfulLogin += LoginController_OnSuccessfulLogin;
            mainMenuObject.SetActive(false);
            loginMenuObject.SetActive(false);
            registerMenuObject.SetActive(false);
            gameModeSelectionMenuObject.SetActive(false);
            if(UserInformation.Instance.isLoggedIn){
                currentMenu = gameModeSelectionMenuObject;
            }
            else{
                currentMenu = mainMenuObject;
            }
            
            currentMenu.SetActive(true);
        }

        private void LoginController_OnSuccessfulLogin(object sender, EventArgs e){
            UserInformation.Instance.isLoggedIn = true;
            previousMenu = currentMenu;
            currentMenu.SetActive(false);
            currentMenu = gameModeSelectionMenuObject;
            currentMenu.SetActive(true);
        }

        /*
        public void LoadScene(string sceneName)
        {
            LevelLoader.Instance.LoadScene(sceneName);
        }*/

        public void LoadBossModeScene(){
            LevelLoader.Instance.LoadScene(1);
        }
        public void LoadParkourModeScene(){
            LevelLoader.Instance.LoadScene(2);
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

