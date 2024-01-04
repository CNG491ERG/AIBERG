using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class EventViewer : MonoBehaviour{
    public string eventPerformer;
    public List<string> eventsBeingPerformed;
    public TextMeshProUGUI text;
    private StringBuilder stringBuilder = new StringBuilder();
    private void FixedUpdate() {
        stringBuilder.Append(eventPerformer + " doing: ");
        foreach(string s in eventsBeingPerformed){
            stringBuilder.Append(s + " ");
        }
        text.SetText(stringBuilder.ToString());
        stringBuilder.Clear();
        eventsBeingPerformed.Clear();
    }

}
