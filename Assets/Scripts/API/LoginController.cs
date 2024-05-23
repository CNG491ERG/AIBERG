using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;
using Codice.Client.Common;
using System.Linq;

namespace AIBERG.API
{
    [Serializable]
    public class LoginResponse
    {
        public string message;
        public int userid;
        public string username;
    }
    public class LoginController : MonoBehaviour
    {

        [SerializeField] public TMP_InputField emailField;
        [SerializeField] public TMP_InputField passwordField;
        [SerializeField] public Button loginButton;
        [SerializeField] public bool loggedInStatus;
        public event EventHandler OnSuccessfulLogin;

        void Start()
        {
            loggedInStatus = false;
            if (emailField == null || passwordField == null)
            {
                Debug.LogWarning("Email or password field has no reference");
            }
            loginButton.onClick.AddListener(TryLogin);
        }

        public void TryLogin()
        {
            StartCoroutine(LoginRequest());
        }

        public IEnumerator LoginRequest()
        {
            if (!string.IsNullOrEmpty(emailField.text) && !string.IsNullOrEmpty(passwordField.text))
            {
                // Create a JSON string containing the username and password
                string jsonData = "{\"email\":\"" + emailField.text + "\", \"password\":\"" + passwordField.text + "\"}";

                // Create a UnityWebRequest for the login endpoint
                UnityWebRequest request = new UnityWebRequest(UserInformation.Instance.loginAddress, "POST");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                // Send the request and wait for the response
                yield return request.SendWebRequest();

                // Check for errors
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + request.error);
                    loggedInStatus = false;
                }
                else
                {
                    // Print the response
                    string response = request.downloadHandler.text;
                    LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);
                    UserInformation.Instance.userID =loginResponse.userid;
                    UserInformation.Instance.playerPlacementBossModeAddress+=UserInformation.Instance.userID.ToString();
                    UserInformation.Instance.playerPlacementParkourModeAddress+=UserInformation.Instance.userID.ToString();
                    UserInformation.Instance.username = loginResponse.username;
                     
                    loggedInStatus = true;
                    OnSuccessfulLogin?.Invoke(this, EventArgs.Empty);
                    
                }
            }
        }
    }
}

