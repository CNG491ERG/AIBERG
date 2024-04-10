using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField emailField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button registerButton;

    void Start()
    {
        if (emailField == null || passwordField == null || usernameField == null)
        {
            Debug.LogWarning("Email or password field has no reference");
        }
        registerButton.onClick.AddListener(TryRegister);
    }

    public void TryRegister()
    {
        if(string.IsNullOrWhiteSpace(emailField.text) || string.IsNullOrWhiteSpace(usernameField.text) || string.IsNullOrWhiteSpace(passwordField.text)){
            Debug.Log("Empty field!");
            return;
        }
        StartCoroutine(RegisterRequest());
    }

    IEnumerator RegisterRequest()
    {
        // Create a JSON string containing the username and password
        string jsonData = "{\"email\":\"" 
                            + emailField.text
                            + "\", \"password\":\""
                            + passwordField.text 
                            + "\", \"username\":\""
                            + usernameField.text
                            + "\"}";

        // Create a UnityWebRequest for the login endpoint
        UnityWebRequest request = new UnityWebRequest("http://34.16.220.152:5000/register", "POST");
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
