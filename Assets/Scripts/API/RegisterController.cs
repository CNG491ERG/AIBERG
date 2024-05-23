using System.Collections;
using System.Collections.Generic;
using System.Text;
using AIBERG.API;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace AIBERG{
public class RegisterController : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField emailField;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField] Button registerButton;
    [Header("Dialog Windows")]
    [SerializeField] DialogWindow emailRegisteredWindow;
    [SerializeField] DialogWindow usernameTooShortWindow;
    [SerializeField] DialogWindow usernameTooLongWindow;
    [SerializeField] DialogWindow passwordTooShortWindow;
    [SerializeField] DialogWindow passwordTooLongWindow;
    [SerializeField] DialogWindow failedConnectionWindow;
    [SerializeField] DialogWindow fieldsEmptyWindow;
    [SerializeField] DialogWindow registrationSuccessWindow;
    [SerializeField] DialogWindow registering;


    void Start()
    {
        if (emailField == null || passwordField == null || usernameField == null)
        {
            Debug.LogWarning("Email or password field has no reference");
        }
    }
    public void TryRegister()
    {
        Debug.Log("hello");
        if(string.IsNullOrWhiteSpace(emailField.text) || string.IsNullOrWhiteSpace(usernameField.text) || string.IsNullOrWhiteSpace(passwordField.text)){
            fieldsEmptyWindow.ShowDialogWindow();
            Debug.Log("if1");
            return;
        }
        if(usernameField.text.Length < 4){
            usernameTooShortWindow.ShowDialogWindow();
            Debug.Log("if2");
            return;
        }
        if(usernameField.text.Length > 16){
            usernameTooLongWindow.ShowDialogWindow();
            Debug.Log("if3");
            return;
        }
        if(passwordField.text.Length < 4){
            passwordTooShortWindow.ShowDialogWindow();
            Debug.Log("if4");
            return;
        }
        if(passwordField.text.Length > 16){
            passwordTooLongWindow.ShowDialogWindow();
            Debug.Log("if5");
            return;
        }
        registering.ShowDialogWindow();
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
        UnityWebRequest request = new UnityWebRequest(UserInformation.Instance.registerAddress, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();
        registering.CloseDialogWindow();
        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            if(request.downloadHandler.text.Contains("already exists")){
                emailRegisteredWindow.ShowDialogWindow();
            }
            else{
                failedConnectionWindow.ShowDialogWindow();
            }
            Debug.Log("Error: " + request.error);
        }
        else
        {
            // Print the response
            registrationSuccessWindow.ShowDialogWindow();
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}

}
