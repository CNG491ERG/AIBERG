using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using AIBERG.API;

namespace AIBERG
{
    [System.Serializable]
    public class LeaderboardEntry
    {
        public string username;
        public int score;
        public string time_taken;
    }

    [System.Serializable]
    public class LeaderboardEntryList
    {
        public LeaderboardEntry[] entries;
    }
public class Leaderboard : MonoBehaviour
    {
        [SerializeField] public GameObject leaderboardContentContainer;
        [SerializeField] private GameObject leaderboardItemPrefab;
        [SerializeField] public bool isForBossMode = true;
        [SerializeField] public bool canBeClosed = true;
        [SerializeField] public Button closeButton;

        private void Start() {
            closeButton.gameObject.SetActive(canBeClosed);
        }
        private void OnEnable()
        {
            StartCoroutine(GetLeaderboardData());
            leaderboardContentContainer.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        }

        IEnumerator GetLeaderboardData()
        {
            string apiUrl = isForBossMode ? UserInformation.Instance.leaderboardBossModeAddress : UserInformation.Instance.leaderboardParkourModeAddress;
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching leaderboard data: " + request.error);
            }
            else
            {
                string jsonResponse = "{\"entries\":" + request.downloadHandler.text + "}";
                LeaderboardEntryList leaderboardData = JsonUtility.FromJson<LeaderboardEntryList>(jsonResponse);
                PopulateLeaderboard(leaderboardData);
            }
        }

        void PopulateLeaderboard(LeaderboardEntryList leaderboardData)
        {
            // Clear existing leaderboard entries
            foreach (Transform child in leaderboardContentContainer.transform)
            {
                Destroy(child.gameObject);
            }
            int i = 0;
            // Create a new entry for each item in the leaderboard data
            foreach (LeaderboardEntry entry in leaderboardData.entries)
            {
                i++;
                GameObject leaderboardEntry = Instantiate(leaderboardItemPrefab, leaderboardContentContainer.transform);
                leaderboardEntry.transform.Find("Text_Placement").GetComponent<TextMeshProUGUI>().text = "#"+i.ToString();
                leaderboardEntry.transform.Find("Text_Username").GetComponent<TextMeshProUGUI>().text = entry.username;
                leaderboardEntry.transform.Find("Text_Score").GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
                leaderboardEntry.transform.Find("Text_RunTime").GetComponent<TextMeshProUGUI>().text = entry.time_taken;
            }
        }

        public void ShowLeaderBoard(){
            this.gameObject.SetActive(true);
        }
        public void CloseLeaderBoard(){
            this.gameObject.SetActive(false);
        }
    }
}
