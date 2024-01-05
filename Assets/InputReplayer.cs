using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Npgsql;
using UnityEngine;

public class InputReplayer : MonoBehaviour
{
    
    private int currentStep = 0;
    [SerializeField] private Faction faction;
    [SerializeField] private EventViewer eventViewer;
    [SerializeField] private Dictionary<int, string> inputs = new Dictionary<int, string>();
    [SerializeField] private int playerId;
    private NpgsqlConnection conn;
    private string connString;
    private void Awake() {
        connString = "Host=34.42.114.162;Port=5432;Username=postgres;Password=postgres;Database=db";
        conn = new NpgsqlConnection(connString);
        try
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT input_data FROM movements WHERE player_id = @playerId";
                cmd.Parameters.AddWithValue("@playerId", playerId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string inputsRetrieved = result.ToString();
                    int step = 1;
                    string[] substrings = inputsRetrieved.Split('\n');

                    foreach (string substring in substrings){
                        if(substring.Length == 4){
                            inputs.Add(step, substring);
                        }
                        step++;
                    }
                }
            }
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Error inserting movement data: " + e.Message);
            conn.Close();
        }
    }
    void Start(){
        eventViewer = GetComponent<EventViewer>();
        faction = transform.parent.GetComponentInChildren<Faction>();
    }

    // Regularly checks and processes input replay during each fixed frame rate update
    void FixedUpdate(){  
        if(inputs.TryGetValue(++currentStep, out string input)){
            ApplyInput(input);
            Debug.Log(input);
        }
    }

    private void ApplyInput(string input){
        faction.BasicAttack.UseAbility(input[1] == '1');
        faction.JumpAbility.UseAbility(input[0] == '1');
        faction.ActiveAbility1.UseAbility(input[2] == '1');
        faction.ActiveAbility2.UseAbility(input[3] == '1');
    }
}
