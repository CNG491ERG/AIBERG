using System.Collections;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text;

namespace AIBERG.API
{
    public class LoginController : MonoBehaviour
    {

        [SerializeField] TMP_InputField emailField;
        [SerializeField] TMP_InputField passwordField;
        [SerializeField] Button loginButton;

        void Start()
        {
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

        IEnumerator LoginRequest()
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
            }
            else
            {
                // Print the response
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}

