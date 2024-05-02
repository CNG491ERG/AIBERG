using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using AIBERG.Core;
using AIBERG.Utilities;

public class StatCollector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameEnvironment environment;
    [SerializeField] private Player player;
    [SerializeField] private Boss boss;
    
    private char winner;
    private long gameLength;
    private float bossHealth;
    private float playerHealth;
    
    private void Start() {
        environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        boss = environment.Boss;
        player = environment.Player;
        player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
        environment.OnMaxStepsReached += Environment_OnMaxStepsReached;
        Debug.Log("Stat Collector Initialized");
    }

    private void OnDestroy() {
        player.OnDamageableDeath -= Player_OnDamageableDeath;
        boss.OnDamageableDeath -= Boss_OnDamageableDeath;
        environment.OnMaxStepsReached -= Environment_OnMaxStepsReached;
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e)
    {
        winner = 'B';
        gameLength = environment.StepCounter;
        bossHealth = boss.Health;
        playerHealth = player.Health;
        WriteStats();
    }

    private void Boss_OnDamageableDeath(object sender, EventArgs e)
    {
        winner = 'P';
        gameLength = environment.StepCounter;
        bossHealth = boss.Health;
        playerHealth = player.Health;
        WriteStats();
    }

    private void Environment_OnMaxStepsReached(object sender, EventArgs e)
    {
        winner = 'N';
        gameLength = environment.MaxSteps;
        bossHealth = boss.Health;
        playerHealth = player.Health;
        WriteStats();
    }

    private void WriteStats()
    {
        string path = Path.Combine(Application.persistentDataPath, "gameStats.txt");
        string stats = $"Winner: {winner}, Game Length: {gameLength}, Boss Health: {bossHealth}, Player Health: {playerHealth}\n";

        // Check if the file exists, if not create it and set headers
        if (!File.Exists(path))
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Winner, Game Length, Boss Health, Player Health");
            }
        }

        // Append the stats to the file
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(stats);
        }

        Debug.Log("Stat written");
    }
}