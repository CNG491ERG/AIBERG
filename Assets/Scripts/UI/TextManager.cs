using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager: MonoBehaviour
{
    public Text textElem;
   
    public void TextEdit(string textValue)
    {
        textElem.text = textValue;
    }
}
