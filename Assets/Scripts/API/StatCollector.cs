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

    private void Start()
    {
        environment = ComponentFinder.FindComponentInParents<GameEnvironment>(this.transform);
        boss = environment.Boss;
        player = environment.Player;
        player.OnDamageableDeath += Player_OnDamageableDeath;
        boss.OnDamageableDeath += Boss_OnDamageableDeath;
        environment.OnMaxStepsReached += Environment_OnMaxStepsReached;
        environment.collectStats = true;
        environment.StartCountingSteps();
        environment.StartCountingMatches();
        bossHealth = boss.MaxHealth;
        playerHealth = player.MaxHealth;
        gameLength = 0;
    }

    private void OnDestroy()
    {
        player.OnDamageableDeath -= Player_OnDamageableDeath;
        boss.OnDamageableDeath -= Boss_OnDamageableDeath;
        environment.OnMaxStepsReached -= Environment_OnMaxStepsReached;
    }

    private void FixedUpdate()
    {
        bossHealth = boss.Health < bossHealth ? boss.Health : bossHealth;
        playerHealth = player.Health < playerHealth ? player.Health : playerHealth;
        gameLength = environment.StepCounter > gameLength ? environment.StepCounter : gameLength;
    }

    private void Player_OnDamageableDeath(object sender, EventArgs e)
    {
        winner = 'B';
        playerHealth = 0;
        WriteStats();
    }

    private void Boss_OnDamageableDeath(object sender, EventArgs e)
    {
        winner = 'P';
        bossHealth = 0;
        WriteStats();
    }

    private void Environment_OnMaxStepsReached(object sender, EventArgs e)
    {
        winner = 'N';
        WriteStats();
    }

    private void WriteStats()
    {
        if (environment.collectStats)
        {
            string envName = environment.gameObject.name;
            string fileName = $"{envName}_Stats.csv";
            string folderPath = Path.Combine(Application.persistentDataPath, "Statistics");

            // Ensure the Statistics folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string path = Path.Combine(folderPath, fileName);
            //string stats = $"Winner: {winner}, Game Length: {gameLength}, Boss Health: {bossHealth}, Player Health: {playerHealth}\n";
            string stats = $"{winner},{gameLength},{bossHealth},{playerHealth}";
            // Check if the file exists, if not create it and set headers
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    //sw.WriteLine("Winner, Game Length, Boss Health, Player Health");
                    sw.WriteLine("Winner,Game Length,Boss Health,Player Health");
                }
            }

            // Append the stats to the file
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(stats);
            }

            bossHealth = boss.MaxHealth;
            playerHealth = player.MaxHealth;
            gameLength = 0;
            Debug.Log($"Stats written to {path}");
        }

    }
}