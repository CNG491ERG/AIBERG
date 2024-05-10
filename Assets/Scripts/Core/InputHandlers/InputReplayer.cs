using System;
using System.Collections.Generic;
using AIBERG.Core.InputHandlers;
using Npgsql;
using UnityEngine;

public class InputReplayer : InputHandler{
    private int currentStep = 0;
    [SerializeField] private Dictionary<int, string> inputs = new Dictionary<int, string>();
    private void Awake() {

    }
    void Start(){
 
    }

    // Regularly checks and processes input replay during each fixed frame rate update
    void FixedUpdate(){  
        if(inputs.TryGetValue(++currentStep, out string input)){
            ApplyInput(input);
            Debug.Log(input);
        }
    }

    private void ApplyInput(string input){
        //faction.BasicAttack.UseAbility(input[1] == '1');
        //faction.JumpAbility.UseAbility(input[0] == '1');
        //faction.ActiveAbility1.UseAbility(input[2] == '1');
        //faction.ActiveAbility2.UseAbility(input[3] == '1');
    }
    
}
