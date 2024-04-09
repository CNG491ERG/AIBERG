
/*using System;
using UnityEngine;
using Npgsql;
using System.Text;

public class InputRecorder : MonoBehaviour{
    /*
    private StringBuilder inputsRecorded = new StringBuilder();
    [SerializeField] private bool startRecording;
    [SerializeField] private int playerId = 1; // Replace with the actual player's ID
    private NpgsqlConnection conn; // Npgsql connection variable
    private string connString;

    // Start is called before the first frame update
    void Start(){
        Debug.Log("connecteed");
        connString = "Host=34.42.114.162;Port=5432;Username=postgres;Password=postgres;Database=db";
        conn = new NpgsqlConnection(connString);
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnGameEnded += GameManager_OnGameEnded;

    }

    private void GameManager_OnGameEnded(object sender, EventArgs e)
    {
        startRecording = false;
        Debug.Log("Record complete, sending to db");
        SendDataToDB(playerId, inputsRecorded.ToString());
    }

    private void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        startRecording = true;
        Debug.Log("Started recording");
    }

    void FixedUpdate(){
        if(startRecording){
            // Capture player movements
            char jumpInput = Input.GetKey(KeyCode.Space) ? '1' : '0';
            char basicAttackInput = Input.GetMouseButton(0) ? '1' : '0';
            char specialAttack1Input = Input.GetKey(KeyCode.Q) ? '1' : '0';
            char specialAttack2Input = Input.GetKey(KeyCode.E) ? '1' : '0';
            string input = $"{jumpInput}{basicAttackInput}{specialAttack1Input}{specialAttack2Input}\n";
            inputsRecorded.Append(input);
        }
        
    }

    private void SendDataToDB(int playerId, string inputsRecorded)
    {
        try
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO movements (player_id, input_data, timestamp) VALUES (@playerId, @movementData, @timeStamp)";
                cmd.Parameters.AddWithValue("playerId", playerId);
                cmd.Parameters.AddWithValue("movementData", inputsRecorded);
                cmd.Parameters.AddWithValue("timeStamp", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Error inserting movement data: " + e.Message);
            conn.Close();
        }
    }

    void OnDestroy()
    {
        if (conn != null && conn.State == System.Data.ConnectionState.Open)
        {
            conn.Close();
            Debug.Log("Disconnected from database.");
        }
    }*/
