using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


namespace AIBERG.API
{
    public class UserInformation : MonoBehaviour
    {
        public string username;
        public int userID;
        public bool playMode;
        public long score = 0;
        public bool win;
        public bool isLocalMode = true;
        public bool isLoggedIn = false;
        public long timetaken;
        public string loginAddress = "https://aiberg.ew.r.appspot.com/login";
        public string registerAddress = "https://aiberg.ew.r.appspot.com/register";
        public string storeMovementAddress = "https://aiberg.ew.r.appspot.com/storeMovements";
        public string leaderboardBossModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/bossmode";
        public string leaderboardParkourModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/parkourmode";
        public string playerPlacementBossModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/bossmode/";
        public string playerPlacementParkourModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/parkourmode/";
        public static UserInformation Instance;
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            if (isLocalMode)
            {
                loginAddress = "http://127.0.0.1:5000/login";
                registerAddress = "http://127.0.0.1:5000/register";
                storeMovementAddress = "http://127.0.0.1:5000/storeMovements";
                leaderboardBossModeAddress = "http://127.0.0.1:5000/leaderboard/bossmode";
                leaderboardParkourModeAddress = "http://127.0.0.1:5000/leaderboard/parkourmode";
                playerPlacementBossModeAddress = "http://127.0.0.1:5000/leaderboard/bossmode/";
                playerPlacementParkourModeAddress = "http://127.0.0.1:5000/leaderboard/parkourmode/";
            }
            else
            {
                loginAddress = "https://aiberg.ew.r.appspot.com/login";
                registerAddress = "https://aiberg.ew.r.appspot.com/register";
                storeMovementAddress = "https://aiberg.ew.r.appspot.com/storeMovements";
                leaderboardBossModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/bossmode";
                leaderboardParkourModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/parkourmode";
                playerPlacementBossModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/bossmode/";
                playerPlacementParkourModeAddress = "https://aiberg.ew.r.appspot.com/leaderboard/parkourmode/";
            }
        }

        void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void ResetUserInformation()
        {
            score = 0;
            timetaken = 0;
            win = false;
        }

        public void SendData()
        {
            StartCoroutine(SendInputDataCoroutine());
        }

        private IEnumerator SendInputDataCoroutine()
        {
            string jsonData = $"{{\"user_id\":{Instance.userID}, \"inputs\":\"\", \"timestamp\":\"{System.DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}\", \"playmode\":\"{Instance.playMode}\", \"score\":\"{Instance.score}\", \"winlose\":{Instance.win.ToString().ToLower()}, \"timetaken\":\"{Instance.timetaken}\"}}";

            Debug.Log("Sending JSON: " + jsonData);

            using (UnityWebRequest request = new UnityWebRequest(Instance.storeMovementAddress, "POST"))
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
                }
            }
        }
    }

}
