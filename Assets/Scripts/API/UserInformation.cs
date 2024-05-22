using UnityEngine;


namespace AIBERG.API
{
    public class UserInformation : MonoBehaviour
    {
        public string username;
        public int userID;
        public bool playMode;
        public int score = 0;
        public bool win;
        public bool isLocalMode = true;
        public long timetaken;
        public string loginAddress = "http://34.16.220.152:5000/login";
        public string registerAddress = "http://34.16.220.152:5000/register";
        public string storeMovementAddress = "http://34.16.220.152:5000/storeMovements";
        public string leaderboardBossModeAddress = "http://34.16.220.152:5000/leaderboard/bossmode";
        public string leaderboardParkourModeAddress = "http://34.16.220.152:5000/leaderboard/parkourmode";
        public string playerPlacementBossModeAddress = "http://34.16.220.152:5000/leaderboard/bossmode/";
        public string playerPlacementParkourModeAddress = "http://34.16.220.152:5000/leaderboard/parkourmode/";
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
        }

        void Start()
        {
            DontDestroyOnLoad(this);
        }


    }

}
