using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System;

namespace AIBERG.API
{
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
                UnityWebRequest request = new UnityWebRequest("http://34.16.220.152:5000/login", "POST");
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
                    Debug.Log("Response: " + request.downloadHandler.text);
                    loggedInStatus = true;
                    OnSuccessfulLogin?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}

