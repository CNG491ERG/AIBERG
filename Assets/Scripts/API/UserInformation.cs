using UnityEngine;


namespace AIBERG.API
{
    public class UserInformation : MonoBehaviour
    {
        public string username;
        public int userID;
        public static UserInformation Instance; 
        private void Awake(){
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            DontDestroyOnLoad(this);
        }


    }

}
