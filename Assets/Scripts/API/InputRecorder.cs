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

        private void Start()
        {
            inputSent = false;
        }

        void FixedUpdate()
        {
            if (environment.IsCountingSteps)
            {
                char jumpInput = environment.Player.inputHandler.JumpInput ? '1' : '0';
                char basicAbilityInput = environment.Player.inputHandler.BasicAbilityInput ? '1' : '0';
                char activeAbility1Input = environment.Player.inputHandler.ActiveAbility1Input ? '1' : '0';
                char activeAbility2Input = environment.Player.inputHandler.ActiveAbility2Input ? '1' : '0';
                char bossMoveUpInput = environment.Boss.GetComponent<BossAgent>().moveUpInput ? '1' : '0';
                char bossMoveDownInput = environment.Boss.GetComponent<BossAgent>().moveDownInput ? '1' : '0';
                char bossBasicAttackInput = environment.Boss.GetComponent<BossAgent>().basicAttackInput ? '1' : '0';
                char bossAttackDroneInput = environment.Boss.GetComponent<BossAgent>().attackDroneInput ? '1' : '0';

                string input = $"{jumpInput}{basicAbilityInput}{activeAbility1Input}{activeAbility2Input}{bossMoveUpInput}{bossMoveDownInput}{bossBasicAttackInput}{bossAttackDroneInput}-";
                inputsRecorded.Append(input);
            }
        }

        public void SendInputData()
        {
            StartCoroutine(SendInputDataCoroutine());
        }

        private IEnumerator SendInputDataCoroutine()
        {
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
