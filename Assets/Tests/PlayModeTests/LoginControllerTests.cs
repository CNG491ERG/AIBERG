using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AIBERG.API;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LoginControllerTests{

    [UnityTest]
    public IEnumerator LoginController_TryLogin_SuccessfulLogin(){
        var emailField = new GameObject();
        emailField.AddComponent<TMP_InputField>();
        
        var passwordField = new GameObject();
        passwordField.AddComponent<TMP_InputField>();

        var loginButton = new GameObject();
        loginButton.AddComponent<Button>();

        var loginController = new GameObject();
        loginController.AddComponent<LoginController>();

        loginController.GetComponent<LoginController>().emailField = emailField.GetComponent<TMP_InputField>();
        loginController.GetComponent<LoginController>().passwordField = passwordField.GetComponent<TMP_InputField>();
        loginController.GetComponent<LoginController>().loginButton = loginButton.GetComponent<Button>();
        yield return null;

        emailField.GetComponent<TMP_InputField>().text = "testuser@test.com";
        passwordField.GetComponent<TMP_InputField>().text = "test123";


        yield return loginController.GetComponent<LoginController>().LoginRequest();
        
        Assert.AreEqual(true, loginController.GetComponent<LoginController>().loggedInStatus);
    }

    [UnityTest]
    public IEnumerator LoginController_TryLogin_InvalidLogin(){
        var emailField = new GameObject();
        emailField.AddComponent<TMP_InputField>();
        
        var passwordField = new GameObject();
        passwordField.AddComponent<TMP_InputField>();

        var loginButton = new GameObject();
        loginButton.AddComponent<Button>();

        var loginController = new GameObject();
        loginController.AddComponent<LoginController>();

        loginController.GetComponent<LoginController>().emailField = emailField.GetComponent<TMP_InputField>();
        loginController.GetComponent<LoginController>().passwordField = passwordField.GetComponent<TMP_InputField>();
        loginController.GetComponent<LoginController>().loginButton = loginButton.GetComponent<Button>();
        yield return null;

        emailField.GetComponent<TMP_InputField>().text = "";
        passwordField.GetComponent<TMP_InputField>().text = "";


        yield return loginController.GetComponent<LoginController>().LoginRequest();
        
        Assert.AreEqual(false, loginController.GetComponent<LoginController>().loggedInStatus);
    }
}
