using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBERG
{
    public class DialogWindow : MonoBehaviour
    {
        private void Start() {
            this.gameObject.SetActive(false);
        }
        public void ShowDialogWindow(){
            this.gameObject.SetActive(true);
        }
        public void CloseDialogWindow(){
            this.gameObject.SetActive(false);
        }
    }
}
