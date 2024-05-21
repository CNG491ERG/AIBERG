using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBERG.Core
{
    public class TestMatchManager : MonoBehaviour {
        [Header("Environments")]
        [SerializeField] private static TestMatchManager _instance;
        [SerializeField] private int completedEnvironments = 0;
        [SerializeField] public int totalEnvironments = 7;

        public static TestMatchManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<TestMatchManager>();
                    if (_instance == null) {
                        GameObject go = new GameObject("TestMatchManager");
                        _instance = go.AddComponent<TestMatchManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        private void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

		private void Start(){
    		Time.timeScale = 10.0f;
    		Application.targetFrameRate = -1;
    		Time.maximumDeltaTime = 0.1f;
			//  Camera.main.enabled = false;		
		}        
        
        public void EnvironmentCompleted() {
            completedEnvironments++;
            Debug.Log($"Completed Envs: {completedEnvironments}");
            if (completedEnvironments >= totalEnvironments) {
                Debug.Log($"Completed Envs at Termination: {completedEnvironments}");
                TerminateGame();
            }
        }

        private void TerminateGame() {
            Debug.Log("All environments completed. Terminating game.");
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
    }
}
