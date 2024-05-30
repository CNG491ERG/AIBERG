using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AIBERG
{
    public class DialogWindow : MonoBehaviour
    {
        public bool isMultipleWindow = false;
        public List<GameObject> allWindows = new List<GameObject>();
        public int windowIndex = 0;
        public GameObject currentWindow;
        public Button previousButton;
        public Button nextButton;
        private void Start()
        {
            if (allWindows.Count > 0)
            {
                currentWindow = allWindows[0];
                currentWindow.SetActive(true);
                previousButton.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);
            }
            if(allWindows.Count == 0){
                if(previousButton != null && nextButton != null){
                    previousButton.gameObject.SetActive(false);
                    nextButton.gameObject.SetActive(false);
                }
            }
        }
        public void ShowDialogWindow()
        {
            this.gameObject.SetActive(true);
        }
        public void CloseDialogWindow()
        {
            this.gameObject.SetActive(false);
        }

        public void SwitchToNextWindow()
        {
            if (windowIndex < allWindows.Count - 1)
            {
                windowIndex++;
                currentWindow.SetActive(false);
                currentWindow = allWindows[windowIndex];
                currentWindow.SetActive(true);
                if (windowIndex == allWindows.Count - 1)
                {
                    nextButton.gameObject.SetActive(false);
                }
                previousButton.gameObject.SetActive(true);
            }
        }

        public void SwitchToPreviousWindow()
        {
            if (windowIndex > 0)
            {
                windowIndex--;
                currentWindow.SetActive(false);
                currentWindow = allWindows[windowIndex];
                currentWindow.SetActive(true);
                if (windowIndex == 0)
                {
                    previousButton.gameObject.SetActive(false);
                }
                nextButton.gameObject.SetActive(true);
            }
        }
    }
}
