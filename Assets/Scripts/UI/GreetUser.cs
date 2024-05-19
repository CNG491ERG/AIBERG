using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AIBERG.API;

namespace AIBERG.UI
{
    public class GreetUser : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private void Update()
        {
            if (!string.IsNullOrEmpty(UserInformation.Instance.username))
            {
                text.text = "Welcome " + UserInformation.Instance.username + "!";
            }
        }
    }
}
