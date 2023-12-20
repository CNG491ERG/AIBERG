using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoInputHandler : MonoBehaviour{
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;

    private void Start() {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
