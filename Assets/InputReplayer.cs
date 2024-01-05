using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class InputReplayer : MonoBehaviour
{
    public Dictionary<int, string> recordedInputs;
    private int currentStep = 0;
    private InputRecorder inputRecorder;
    string filePath;

    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;
    public bool isEnabled = false;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private event EventHandler OnCloudSaveLoaded;
    void Start()
    {
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
        inputRecorder = FindObjectOfType<InputRecorder>();
        filePath = Path.Combine(Application.persistentDataPath, "myFile.txt");
    }

    // Regularly checks and processes input replay during each fixed frame rate update
    void FixedUpdate()
    {   
        if(Input.GetKeyDown(KeyCode.T)){

        }

        // If replay is enabled, apply the recorded input at the current step
        if (isEnabled)
        {
            playerInputHandler.enabled = false;
            if (recordedInputs.TryGetValue(currentStep, out string input))
            {
                ApplyInput(input);
                currentStep++;
            }
        }
    }

    private void ApplyInput(string input)
    {
        faction.BasicAttack.UseAbility(input[1] == '1');
        faction.JumpAbility.UseAbility(input[0] == '1');
        faction.ActiveAbility1.UseAbility(input[2] == '1');
        faction.ActiveAbility2.UseAbility(input[3] == '1');
    }
}
