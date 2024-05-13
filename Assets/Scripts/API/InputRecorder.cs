using UnityEngine;
using System.Text;
using UnityEngine.Networking;
using System.Collections;
using AIBERG.Core;

namespace AIBERG.API
{
    public class InputRecorder : MonoBehaviour
    {
        [SerializeField] private bool inputSent;
        [SerializeField] private GameEnvironment environment;
        private StringBuilder inputsRecorded = new StringBuilder();
        private float bossLocalPosition;
        private float playerLocalPosition;

        private void Start()
        {
            inputSent = false;
        }

        void FixedUpdate()
        {
            if (environment.IsCountingSteps)
            {
                char basicAbilityInput = environment.Player.inputHandler.BasicAbilityInput ? '1' : '0';
                char activeAbility1Input = environment.Player.inputHandler.ActiveAbility1Input ? '1' : '0';
                char activeAbility2Input = environment.Player.inputHandler.ActiveAbility2Input ? '1' : '0';
                char bossBasicAttackInput = environment.Boss.GetComponent<BossAgent>().basicAttackInput ? '1' : '0';
                char bossAttackDroneInput = environment.Boss.GetComponent<BossAgent>().attackDroneInput ? '1' : '0';
                bossLocalPosition = environment.Boss.transform.localPosition.y;
                playerLocalPosition = environment.Player.transform.localPosition.y;
                string input = $"{playerLocalPosition};{bossLocalPosition};{basicAbilityInput}{activeAbility1Input}{activeAbility2Input}{bossBasicAttackInput}{bossAttackDroneInput}*";
                inputsRecorded.Append(input);
            }
        }

        public void SendInputData()
        {
            StartCoroutine(SendInputDataCoroutine());
        }

        private IEnumerator SendInputDataCoroutine()
        {
            for(int i = 0; i<50; i++){
                string input = $"{playerLocalPosition};{bossLocalPosition};00000*";
                inputsRecorded.Append(input);
            }
            string jsonData = $"{{\"user_id\":{UserInformation.Instance.userID}, \"inputs\":\"{inputsRecorded.ToString()}\", \"timestamp\":\"{System.DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}\", \"playmode\":\"{UserInformation.Instance.playMode}\", \"score\":\"{UserInformation.Instance.score}\", \"winlose\":{UserInformation.Instance.win.ToString().ToLower()}, \"timetaken\":\"{UserInformation.Instance.timetaken}\"}}";

            Debug.Log("Sending JSON: " + jsonData);

            using (UnityWebRequest request = new UnityWebRequest(UserInformation.Instance.storeMovementAddress, "POST"))
            {
                byte[] jsonToSend = new UTF8Encoding().GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + request.error);
                }
                else
                {
                    Debug.Log("Response: " + request.downloadHandler.text);
                    inputSent = true;
                }
            }
        }
    }
}
