using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using AIBERG.API;
using System.Collections.Generic;

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
        [SerializeField] public bool showYouPlacedText = false;
        [SerializeField] public TextMeshProUGUI youPlacedText;
        [SerializeField] public Button closeButton;
        [SerializeField] List<GameObject> entries = new List<GameObject>();

        private void Start()
        {
            closeButton.gameObject.SetActive(canBeClosed);
        }

        private void FixedUpdate() {
            if(showYouPlacedText && this.isActiveAndEnabled){
                if(UserInformation.Instance.placement == 0){
                    UserInformation.Instance.GetPlayerPlacement();
                }
                else{
                    if(UserInformation.Instance.placement <= entries.Count){
                        if(isForBossMode){
                            var entry = entries[UserInformation.Instance.placement-1];
                            entry.transform.Find("Image (1)").GetComponent<Image>().color = new Color(201.0f / 255.0f, 35.0f/255.0f, 32.0f/255.0f);
                            
                        }
                        else{
                            var entry = entries[UserInformation.Instance.placement-1];
                            entry.transform.Find("Image (1)").GetComponent<Image>().color = new Color(233.0f/255.0f, 179.0f/255.0f, 91.0f/255.0f);
                        }
                    }
                    youPlacedText.text = "You ranked " + GetOrdinalString(UserInformation.Instance.placement) + " place.";
                }
            }    
        }

        IEnumerator GetLeaderboardData()
        {
            if (showYouPlacedText)
            {
                youPlacedText.gameObject.SetActive(true);
                youPlacedText.text = "";
            }
            else{
                youPlacedText.gameObject.SetActive(false);
            }
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
                entries.Add(leaderboardEntry);
                leaderboardEntry.transform.Find("Text_Placement").GetComponent<TextMeshProUGUI>().text = "#" + i.ToString();
                leaderboardEntry.transform.Find("Text_Username").GetComponent<TextMeshProUGUI>().text = entry.username;
                leaderboardEntry.transform.Find("Text_Score").GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
                leaderboardEntry.transform.Find("Text_RunTime").GetComponent<TextMeshProUGUI>().text = entry.time_taken;
            }
        }
        public string GetOrdinalString(int number)
        {
            if (number <= 0) return number.ToString(); // Handle non-positive numbers if needed

            // Determine the suffix
            string suffix;
            int lastDigit = number % 10;
            int lastTwoDigits = number % 100;

            if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
            {
                suffix = "th";
            }
            else
            {
                switch (lastDigit)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
            }

            // Return the number with its suffix
            return number.ToString() + suffix;
        }

        public void ShowLeaderBoard()
        {
            this.gameObject.SetActive(true);
            StartCoroutine(GetLeaderboardData());
            leaderboardContentContainer.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
        }
        public void CloseLeaderBoard()
        {
            this.gameObject.SetActive(false);
        }
    }
}
