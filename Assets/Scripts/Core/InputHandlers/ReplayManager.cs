using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace AIBERG.Core.InputHandlers
{
    [Serializable]
    public class Playthrough
    {
        public int playthroughID;
        public string playthrough;
        public Dictionary<int, string> stepInputs = new Dictionary<int, string>();

        public void ParseStepInputs()
        {
            string[] splits = playthrough.Split('-');
            for (int i = 0; i < splits.Length; i++)
            {
                if (!string.IsNullOrEmpty(splits[i]))
                {
                    stepInputs[i] = splits[i];
                }
            }
        }
    }

    [Serializable]
    public class PlaythroughArray
    {
        public Playthrough[] playthroughs; // This will hold the array of playthroughs
    }
    public class ReplayManager : MonoBehaviour
    {
        public Queue<Playthrough> playthroughs = new Queue<Playthrough>();
        public string getTopPlaythroughAddress = "http://34.16.220.152:5000/getTopPlaythroughs";
        public bool isLocalMode = true;
        public bool queueReady = false;

        private void Awake()
        {
            if (isLocalMode)
            {
                getTopPlaythroughAddress = "http://localhost:5000/getTopPlaythroughs";
            }
        }

        void Start()
        {
            StartCoroutine(GetTopPlaythroughs());
        }
        IEnumerator GetTopPlaythroughs()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(getTopPlaythroughAddress))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log("Received: " + webRequest.downloadHandler.text);
                    ProcessJsonData(webRequest.downloadHandler.text);
                }
            }
        }

        private void ProcessJsonData(string text)
        {
            try
            {
                // Parse the JSON data
                PlaythroughArray topPlaythroughs = JsonUtility.FromJson<PlaythroughArray>("{\"playthroughs\":" + text + "}");
                int i = 0;

                // Use the data in your game (example: print data)
                foreach (var playthrough in topPlaythroughs.playthroughs)
                {
                    playthrough.playthroughID = i;
                    playthrough.ParseStepInputs();
                    i++;
                    playthroughs.Enqueue(playthrough);
                }

                if (playthroughs.Count > 0)
                {
                    queueReady = true;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error parsing JSON: " + e.Message);
            }
        }



    }
}
