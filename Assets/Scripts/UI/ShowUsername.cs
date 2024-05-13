using System.Collections;
using System.Collections.Generic;
using AIBERG.API;
using TMPro;
using UnityEngine;

namespace AIBERG.UI
{
    public class ShowUsername : MonoBehaviour
    {
        public TextMeshProUGUI text;
        private void Start()
        {
            if (UserInformation.Instance != null)
            {
                if (!string.IsNullOrEmpty(UserInformation.Instance.username))
                {
                    text.text = UserInformation.Instance.username;
                }
            }

        }
    }
}
