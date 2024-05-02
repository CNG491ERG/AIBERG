
using System;
using UnityEngine;
using Npgsql;
using System.Text;
using AIBERG.Core;
using AIBERG.API;
using System.Collections;
using UnityEngine.Networking;

public class InputRecorder : MonoBehaviour{
    [SerializeField] public GameEnvironment environment;
    private StringBuilder inputsRecorded = new StringBuilder();
    [SerializeField] private int playerId = UserInformation.Instance.userID;

    void FixedUpdate(){
        if(environment.IsCountingSteps){
            // Capture player movements
            char jumpInput = environment.Player.inputHandler.JumpInput ? '1' : '0';
            char basicAbilityInput = environment.Player.inputHandler.BasicAbilityInput ? '1' : '0';
            char activeAbility1Input = environment.Player.inputHandler.ActiveAbility1Input ? '1' : '0';
            char activeAbility2Input = environment.Player.inputHandler.ActiveAbility2Input ? '1' : '0';
            string input = $"{jumpInput}{basicAbilityInput}{activeAbility1Input}{activeAbility2Input}\n";
            inputsRecorded.Append(input);
        }
    }


    // Call this method when you want to send the data, for example, at the end of a game level
    public void SendInputData() {
        StartCoroutine(SendInputDataCoroutine());
    }

    private IEnumerator SendInputDataCoroutine() {
        string jsonData = $"{{\"user_id\":{UserInformation.Instance.userID}, \"inputs\":\"{inputsRecorded.ToString()}\"}}";
        
        Debug.Log(jsonData);
        UnityWebRequest request = new UnityWebRequest(UserInformation.Instance.storeMovementAddress, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Input data send error: " + request.error);
        } else {
            Debug.Log("Input data sent successfully.");
        }
    }
}