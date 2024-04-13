using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingOrigin : MonoBehaviour
{
    public Transform[] environmentObjects; // Array of environment objects to reset
    public int treshold = 100;

    public Transform Camera;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Camera != null && Camera.transform.position.x > treshold)
        {
            ResetEnvironment();
        }
    }

    private void ResetEnvironment()
    {
        for (int i = 0; i < environmentObjects.Length; i++)
        {
            Vector3 newPosition = environmentObjects[i].position;               // obj position
            newPosition.x = newPosition.x - Camera.transform.position.x;        //0+ its position to camera now (since camera will spawn at 0)
            environmentObjects[i].position = newPosition;   
        }

    }
}
