using System;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;

public class InputRecorder : MonoBehaviour
{
    private Dictionary<int, string> inputList = new Dictionary<int, string>();
    private int stepCounter = 0;
    private int playerId = 1; // Replace with the actual player's ID

    private NpgsqlConnection conn; // Npgsql connection variable
    // Start is called before the first frame update
    void Start()
    {
        string connString = "Host=34.42.114.162;Port=5432;Username=postgres;Password=postgres;Database=db";

        using var conn = new NpgsqlConnection(connString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM \"Player\"", conn);

        using NpgsqlDataReader reader = cmd.ExecuteReader();
        
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Debug.Log(reader[i].ToString());
            }

        }
    }

    void FixedUpdate()
    {
        // Capture player movements
        char jumpInput = Input.GetKey(KeyCode.Space) ? '1' : '0';
        char basicAttackInput = Input.GetMouseButton(0) ? '1' : '0';
        char specialAttack1Input = Input.GetKey(KeyCode.Q) ? '1' : '0';
        char specialAttack2Input = Input.GetKey(KeyCode.E) ? '1' : '0';
        string input = $"{jumpInput}{basicAttackInput}{specialAttack1Input}{specialAttack2Input}";
        inputList.Add(stepCounter, input);
        stepCounter++;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StoreMovementsInDatabase(inputList);
        }
    }

    private void StoreMovementsInDatabase(Dictionary<int, string> dict)
    {
        try
        {
            string serializedData = SerializeDictionaryToJson(dict);
            InsertMovementData(playerId, serializedData);
            Debug.Log("Player movements stored in the database.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to store movements: " + e.Message);
        }
    }

    private string SerializeDictionaryToJson(Dictionary<int, string> dict)
    {
        return JsonUtility.ToJson(new Serialization<Dictionary<int, string>>(dict));
    }

    private void InsertMovementData(int playerId, string serializedData)
    {
        try
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Movements (player_id, input_data, timestamp) VALUES (@playerId, @movementData, @timeStamp)";
                cmd.Parameters.AddWithValue("playerId", playerId);
                cmd.Parameters.AddWithValue("movementData", serializedData);
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
    }

    [Serializable]
    private class Serialization<T>
    {
        public Serialization(T data)
        {
            this.data = data;
        }

        public T data;
    }
}
