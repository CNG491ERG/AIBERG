using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIBERG
{
    public class DialogWindow : MonoBehaviour
    {
        public void ShowDialogWindow(){
            this.gameObject.SetActive(true);
        }
        public void CloseDialogWindow(){
            this.gameObject.SetActive(false);
        }
    }
}
